using UnityEngine;

[AddComponentMenu("ProTubeVR/ProTubeManager")]
public class ProTubeManager : MonoBehaviour
{
    public static ProTubeManager Instance { get; private set; } // Uncommented this line
    public bool dontDestroyOnLoad = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (dontDestroyOnLoad){
                 DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ForceTubeVRInterface.InitAsync(false);
        // ForceTubeVRInterface.LoadChannelJSon();
    }

    // DEBUG METHODS
    public void D_Shoot()
    {
        ForceTubeVRInterface.Shoot(255, 255, 0.5f, ForceTubeVRChannel.all);
    }
}