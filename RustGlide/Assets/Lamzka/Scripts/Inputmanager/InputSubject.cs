using System.Collections.Generic;
using UnityEngine;

public class InputSubject : MonoBehaviour
{
    private List<IPlayerInput> InputObserver = new List<IPlayerInput>();

    public void AddObserver(IPlayerInput Observer)
    {
        InputObserver.Add(Observer);
    }

    public void RemoveObserver(IPlayerInput Observer)
    {
        InputObserver.Remove(Observer);
    }

    protected void NotifyTriggerRValue(bool RState)
    {
        InputObserver.ForEach((InputObserver) => InputObserver.RightTrigger(RState));
    }

    protected void NotifyTriggerLValue(bool LState)
    {
        InputObserver.ForEach((InputObserver) => InputObserver.LeftTrigger(LState));
    }



}
