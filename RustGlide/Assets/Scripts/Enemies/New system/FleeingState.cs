using UnityEngine;

public class FleeingState : IState
{
    public void OnEnter(StateController controller)
    {
        // "What was that!?"
    }

    public void UpdateState(StateController controller)
    {
        // Search for player
    }

    public void OnHurt(StateController controller)
    {
        // Transition to Hurt State
    }

    public void OnExit(StateController controller)
    {
        // "Must've been the wind"
    }

    public void FixedUpdateState(StateController controller)
    {
        throw new System.NotImplementedException();
    }
}
