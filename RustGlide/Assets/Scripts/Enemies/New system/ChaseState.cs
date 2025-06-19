using System;
using System.Collections;
using UnityEngine;

public class ChaseState : IState
{
    
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 targetVelocity;
    private float rotationSpeed = 160;
    private float predictionTime = 0.5f;

    public void OnEnter(StateController controller)
    {
        // Initialize positions to avoid velocity spike
        currentPosition = previousPosition = controller.Target.transform.position;
    }

    public void UpdateState(StateController controller)
    {
        // If in attack range AND we can see the player, switch state
        bool distanceCheck = Vector3.Distance(controller.transform.position, controller.Target.transform.position) <= controller.AttackRange;
        if (distanceCheck && CanSeeTarget(controller))
        {
            controller.ChangeState(controller.attackState);
            return;
        }

        // Stores last position
        previousPosition = currentPosition;
        currentPosition = controller.transform.position;

        // 1. get player velocity and get predicted position of player = target position
        Vector3 desiredVelocity = Pursue(controller, Vector3.zero);

        // 2. shoot rays to target position and in circle around yourself
        // 3. Either (1) take average of rays that did not hit and go there

        // Calculate wanted velocity
        Vector3 avoidanceVector = GetAvoidanceVector(controller);

        // Set velocity of enemy
        float rotationAmount = rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
        targetVelocity = Vector3.RotateTowards(controller.rb.linearVelocity, avoidanceVector.normalized * controller.maxSpeed, rotationAmount, 1000f);
        
    }

    private bool CanSeeTarget(StateController controller)
    {
        Vector3 direction = (controller.Target.transform.position - controller.transform.position);
        bool blocked = Physics.Raycast(controller.transform.position, direction, out var hit, controller.VisionRadius);
        if (blocked && hit.collider.gameObject == controller.Target) return true;
        return false ;
    }

    public void OnHurt(StateController controller) { }
    public void OnExit(StateController controller) { }

    private Vector3 Pursue(StateController controller, Vector3 velocity)
    {
        // Predict future target position with velocity
        Vector3 prediction = controller.Target.transform.position + velocity * predictionTime;
        return Seek(controller, prediction);
    }

    private Vector3 Seek(StateController controller, Vector3 prediction)
    {
        // Desired = targetPos - currentPos
        Vector3 desired = prediction - controller.transform.position;
        float speed = desired.magnitude;
        
        // Caps speed when bigger than maxSpeed
        if (speed > controller.maxSpeed)
            desired = desired.normalized * controller.maxSpeed;

        if (speed < controller.minSpeed)
            desired = desired.normalized * controller.maxSpeed * 2;

        // Calculate steeringforce
        Vector3 steering = desired - controller.rb.linearVelocity;
        Vector3 force = controller.maxSpeed * steering;


        // Rotate to face movement direction
        if (desired.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desired, Vector3.up);
            controller.graphics.transform.rotation = Quaternion.Slerp(
                controller.graphics.transform.rotation,
                targetRotation,
                Time.deltaTime * 10f
            );
        }

        return force;
    }

    private Vector3 GetAvoidanceVector(StateController controller)
    {

        Vector3 weightedAverageFreeRay = Vector3.zero;

        // Direction to target
        Vector3 startVector = (controller.Target.transform.position - controller.transform.position);
        weightedAverageFreeRay += RaysOnPlane(controller, weightedAverageFreeRay, new Vector3(startVector.x, 0f, startVector.z).normalized, Vector3.up);
        weightedAverageFreeRay += RaysOnPlane(controller, weightedAverageFreeRay, new Vector3(0f, startVector.y, startVector.z).normalized, Vector3.right);
        weightedAverageFreeRay += RaysOnPlane(controller, weightedAverageFreeRay, new Vector3(startVector.x, startVector.y, 0f).normalized, Vector3.forward);

        weightedAverageFreeRay = weightedAverageFreeRay.normalized;
        Debug.DrawRay(controller.transform.position, weightedAverageFreeRay * 5f, Color.blue);
        return weightedAverageFreeRay;
    }

    private Vector3 RaysOnPlane(StateController controller, Vector3 weightedAverageFreeRay, Vector3 startVector, Vector3 rotationAxis)
    {
        int rayCount = 8;
        float rayDistance = 3f;
        float minimalRayWeight = 0.5f;

        // Shoots rays around enemy and when it hits an object returns force in opposite direction
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount);
            // 0 = 1
            // 180 = 0.5
            // 360 = 1
            float weight = minimalRayWeight +                 // Elke ray telt een beetje mee!
                           Mathf.Abs(180f - angle) / 180f;   // Hoever zit de ray qua graden van onze "main" ray af? (getal tussen 0 en 1)

            // Rotation on the correct axis (up, right or forward) * the start vector, rotates the start vector the angle along the axis.
            Vector3 direction = Quaternion.Euler(angle * rotationAxis) * startVector;

            RaycastHit hit;
            bool blocked = Physics.Raycast(controller.transform.position, direction, out hit, rayDistance);
            Debug.DrawRay(controller.transform.position, direction * (blocked ? (hit.distance) : rayDistance), blocked ? Color.red : Color.green);
            if (blocked)
            {
                // The higher the distance, the lower the weight should be because we are still far away from collision
                // The lower the distance, the higher the weight because we are almost hitting something.
                float distanceWeight = 1.0f - hit.distance / (float)rayDistance;
                weightedAverageFreeRay -= direction * weight * distanceWeight;
            }
            else
            {
                weightedAverageFreeRay += direction * weight;
            }
        }

        return weightedAverageFreeRay;
    }

    public void FixedUpdateState(StateController controller)
    {
        controller.rb.linearVelocity = targetVelocity;
    }
}
