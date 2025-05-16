using UnityEngine;

public class ChaseState : IState
{
    public void OnEnter(StateController controller)
    {
        // "What was that!?"
    }

    public void UpdateState(StateController controller)
    {
        controller.agent.SetDestination(controller.Target.transform.position);

        if (Vector3.Distance(controller.transform.position, controller.Target.transform.position) <= 1.4f)
        {
            controller.ChangeState(controller.attackState);
        }
    }

    public void OnHurt(StateController controller)
    {
        // Transition to Hurt State
    }

    public void OnExit(StateController controller)
    {
        // "Must've been the wind"
    }
}
