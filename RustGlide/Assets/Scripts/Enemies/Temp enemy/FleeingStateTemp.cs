using UnityEngine;

public class FleeingStateTemp : IStateTemp
{
    public void OnEnter(StateControllerTemp controller)
    {
        // "What was that!?"
    }

    public void UpdateState(StateControllerTemp controller)
    {
        // Search for player
    }

    public void OnHurt(StateControllerTemp controller)
    {
        // Transition to Hurt State
    }

    public void OnExit(StateControllerTemp controller)
    {
        // "Must've been the wind"
    }
}
