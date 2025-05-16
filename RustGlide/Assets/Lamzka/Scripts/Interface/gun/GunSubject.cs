using System.Collections.Generic;

using UnityEngine;

public class GunSubject : MonoBehaviour
{
    private List<IGunGetState> _GunObserver = new List<IGunGetState>();

    public void AddObserver(IGunGetState Observer)
    {
        _GunObserver.Add(Observer);
    }

    public void RemoveObserver(IGunGetState Observer)
    {
        _GunObserver.Remove(Observer);
    }

    protected void NotifyIsGrabbed(bool IsGrabbed)
    {
        _GunObserver.ForEach((_GunObserver) => _GunObserver.NotifyGrab(IsGrabbed));
    }


}

