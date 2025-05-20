using GorillaLocomotion;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UpgradeGorilla : MonoBehaviour
{
    public void Upgrade()
    {
        XROrigin GorillaPlayer = FindFirstObjectByType<XROrigin>();
        GorillaPlayer.GetComponentInChildren<Player>().jumpMultiplier++;
    }
}
