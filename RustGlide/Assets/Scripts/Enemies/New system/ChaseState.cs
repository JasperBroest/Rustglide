using System.Collections;
using UnityEngine;

public class ChaseState : IState
{
    public float maxSpeed = 5f;
    public float maxForce = 2f;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 velocity;

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

        // Predict future position
        Vector3 predictedPosition = currentPosition + velocity * predictionTime;

        // If in attack range, switch state
        if (Vector3.Distance(controller.transform.position, controller.Target.transform.position) <= 1.4f)
        {
            controller.ChangeState(controller.attackState);
            controller.TargetRigidbody.linearVelocity = Vector3.zero;
            return;
        }

        // Apply pursuit force
        controller.rb.AddForce(Pursue(controller, velocity));
    }

    public void OnHurt(StateController controller) { }
    public void OnExit(StateController controller) { }

    private Vector3 Seek(StateController controller, Vector3 prediction)
    {
        Vector3 desired = prediction - controller.transform.position;
        float speed = desired.magnitude;

        if (speed > maxSpeed)
            desired = desired.normalized * maxSpeed;

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

    private Vector3 Pursue(StateController controller, Vector3 velocity)
    {
        Vector3 prediction = controller.Target.transform.position + velocity * predictionTime;
        return Seek(controller, prediction);
    }
}
