using System;
using System.Collections;
using UnityEngine;

public class ChaseState : IState
{
    public float maxSpeed = 5f;
    public float minSpeed = 3f;
    public float maxForce = 2f;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 velocity;
    bool foundOpen = false;

    private float predictionTime = 0.5f;

    public void OnEnter(StateController controller)
    {
        // Initialize positions to avoid velocity spike
        currentPosition = previousPosition = controller.Target.transform.position;
    }

    public void UpdateState(StateController controller)
    {
        // Update positions and calculate velocity
        previousPosition = currentPosition;
        currentPosition = controller.Target.transform.position;
        velocity = (currentPosition - previousPosition) / Time.deltaTime;

        // If in attack range, switch state
        if (Vector3.Distance(controller.transform.position, controller.Target.transform.position) <= controller.AttackRange)
        {
            controller.ChangeState(controller.attackState);
            return;
        }

        // Try to avoid terrain, otherwise pursue
        Vector3 avoidance = AvoidTerrain(controller);

        // Only use avoidance if a clear path is found, otherwise always pursue
        if (foundOpen)
        {
            controller.rb.AddForce(avoidance);
        }
        else
        {
            controller.rb.AddForce(Pursue(controller, velocity));
        }
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

    private Vector3 AvoidTerrain(StateController controller)
    {
        int rayCount = 21;
        float spreadAngle = 240f;
        float rayDistance = 1f;
        Vector3 toTarget = (controller.Target.transform.position - controller.transform.position).normalized;

        // Always start with pursuit as the default direction
        Vector3 bestDirection = toTarget;
        float bestAlignment = float.MinValue;
        bool openFound = false;


        for (int i = 0; i < rayCount; i++)
        {
            float angle = -spreadAngle / 2 + (spreadAngle / (rayCount - 1)) * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * controller.transform.forward;

            RaycastHit hit;
            bool blocked = Physics.Raycast(controller.transform.position, direction, out hit, rayDistance);
            if (hit.collider != null)
            {
                if (!hit.collider.CompareTag("player"))
                {
                    if (blocked)
                    {
                        float alignment = Vector3.Dot(direction, toTarget);
                        if (alignment > bestAlignment)
                        {
                            bestAlignment = alignment;
                            bestDirection = direction;
                            return -direction;
                        }
                    }
                    else
                    {
                        openFound = true;
                        return direction.normalized;
                    }
                }
                else
                {
                    Pursue(controller, velocity);
                }
            }

            Debug.DrawRay(controller.transform.position, direction * (blocked ? (hit.distance) : rayDistance), blocked ? Color.red : Color.green);
        }

        foundOpen = openFound;

        // If no open direction found, just keep pursuing the player (don't stop)
        return bestDirection.normalized * maxForce;
    }
}
