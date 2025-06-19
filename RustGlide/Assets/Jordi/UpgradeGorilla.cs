using GorillaLocomotion;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UpgradeGorilla : MonoBehaviour
{
    public void Upgrade()
    {
        XROrigin GorillaPlayer = FindFirstObjectByType<XROrigin>();
        if(GorillaPlayer.GetComponentInChildren<Player>().jumpMultiplier < 20)
        {
            GorillaPlayer.GetComponentInChildren<Player>().jumpMultiplier++;
        }
        //Chosen.instance.DeactivateCard();
    }
}
