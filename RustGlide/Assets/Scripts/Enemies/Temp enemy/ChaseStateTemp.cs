using System;
using System.Collections;
using UnityEngine;

public class ChaseStateTemp : IStateTemp
{
    public float maxSpeed = 5f;
    public float minSpeed = 3f;
    public float maxForce = 2f;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 velocity;
    bool foundOpen = false;

    private float predictionTime = 0.5f;

    public void OnEnter(StateControllerTemp controller)
    {
        // Initialize positions to avoid velocity spike
        currentPosition = previousPosition = controller.Target.transform.position;
    }

    public void UpdateState(StateControllerTemp controller)
    {
        controller.Agent.SetDestination(controller.Target.transform.position);

        // If in attack range, switch state
        if (Vector3.Distance(controller.transform.position, controller.Target.transform.position) <= controller.AttackRange)
        {
            controller.ChangeState(controller.attackStateTemp);
            return;
        }
    }

    public void OnHurt(StateControllerTemp controller) { }
    public void OnExit(StateControllerTemp controller) { }
}
