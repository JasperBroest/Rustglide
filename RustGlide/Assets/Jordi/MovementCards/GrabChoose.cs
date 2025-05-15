using Unity.XR.CoreUtils;
using UnityEngine;

public class GrabChoose : MonoBehaviour
{   
    public void OnGrab()
    {
        FindFirstObjectByType<Chosen>().HasChosen = true;
        this.gameObject.SetActive(false);
    }

    public void ActivateGorilla()
    {
        
    }
}
