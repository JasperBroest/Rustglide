using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Chosen : MonoBehaviour
{
    public static Chosen instance;

    public bool hasChosen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DeactivateCard()
    {
        hasChosen = true;
    }
}
