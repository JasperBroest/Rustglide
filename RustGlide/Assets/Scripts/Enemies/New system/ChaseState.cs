using System;
using System.Collections;
using UnityEngine;

public class ChaseState : IState
{
    public float maxSpeed = 5f;
    public float minSpeed = 3f;
    public float maxForce = 4f;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 targetVelocity;

    private float predictionTime = 0.5f;

    public void OnEnter(StateController controller)
    {
        // Initialize positions to avoid velocity spike
        currentPosition = previousPosition = controller.Target.transform.position;
    }

    public void UpdateState(StateController controller)
    {
        // If in attack range, switch state
        if (Vector3.Distance(controller.transform.position, controller.Target.transform.position) <= controller.AttackRange)
        {
            controller.ChangeState(controller.attackState);
            return;
        }

        // Stores last position
        previousPosition = currentPosition;
        currentPosition = controller.transform.position;

        if (Vector3.Distance(previousPosition, currentPosition) < 0.01f)
        {
            Debug.Log("STUCK!!!!!!");
        }

        // 1. get player velocity and get predicted position of player = target position
        Vector3 desiredVelocity = Pursue(controller, Vector3.zero);

        // 2. shoot rays to target position and in circle around yourself
        // 3. Either (1) take average of rays that did not hit and go there

        // Calculate wanted velocity
        Vector3 avoidanceVector = GetAvoidanceVector(controller);
        targetVelocity = avoidanceVector.normalized * maxForce;

        // Set velocity of enemy
        controller.rb.linearVelocity = targetVelocity;
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
        if (speed > maxSpeed)
            desired = desired.normalized * maxSpeed;

        if (speed < minSpeed)
            desired = desired.normalized * maxSpeed * 2;

        // Calculate steeringforce
        Vector3 steering = desired - controller.rb.linearVelocity;
        Vector3 force = maxForce * steering;


        // Rotate to face movement direction
        if (desired.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desired, Vector3.up);
            controller.transform.rotation = Quaternion.Slerp(
                controller.transform.rotation,
                targetRotation,
                Time.deltaTime * 10f
            );
        }

        return force;
    }

    private Vector3 GetAvoidanceVector(StateController controller)
    {
        int rayCount = 17;
        float rayDistance = 2f;
        float minimalRayWeight = 0.5f;

        Vector3 weightedAverageFreeRay = Vector3.zero;

        // Direction to target
        Vector3 startVector = (controller.Target.transform.position - controller.transform.position);
        startVector.y += 0.5f;
        startVector.Normalize();

        // Shoots rays around enemy and when it hits an object returns force in opposite direction
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount);
            // 0 = 1
            // 180 = 0.5
            // 360 = 1
            float weight = minimalRayWeight +                 // Elke ray telt een beetje mee!
                           Mathf.Abs(angle - 180f) / 180f;   // Hoever zit de ray qua graden van onze "main" ray af? (getal tussen 0 en 1)
                            
            Vector3 direction = Quaternion.Euler(0, angle, 0) * startVector;

            RaycastHit hit;
            bool blocked = Physics.Raycast(controller.transform.position, direction, out hit, rayDistance);
            Debug.DrawRay(controller.transform.position, direction * (blocked ? (hit.distance) : rayDistance), blocked ? Color.red : Color.green);
            if (blocked) continue;

            weightedAverageFreeRay += direction * weight;
        }

        weightedAverageFreeRay = weightedAverageFreeRay.normalized;
        Debug.DrawRay(controller.transform.position, weightedAverageFreeRay * 5f, Color.blue);
        return weightedAverageFreeRay; 
    }

    public void FixedUpdateState(StateController controller)
    {
        
    }
}
