using UnityEngine;




[CreateAssetMenu(fileName = "ProTubeSettings", menuName = "ProTube/ProTubeSettings")]
public class ProTubeSettings : ScriptableObject
{
    public string name = "ProTube Settings";
    [Header("ProTube Settings")]
    [Range(0, 255)]
    public byte kickPower = 255;
    [Range(0, 255)]
    public byte rumblePower = 255;
    [Range(0.1f, 1f)]
    public float rumbleDuration = 0.5f;

    public float tempo = 0.5f; // Default tempo value
    public bool useTempo = false; // Default tempo usage value

    // getter kickPower
    public byte GetKickPower()
    {
        if (useTempo)
        {
            return ForceTubeVRInterface.TempoToKickPower(tempo);
        }
        else
        {
            return kickPower;
        }
    }

    // Automatically set the name based on the filename
    // Set the name based on the filename when the asset is created
    private void OnEnable()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(name) || name == "ProTube Settings")
        {
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
            string assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            name = assetName;
        }
#endif
    }


}
