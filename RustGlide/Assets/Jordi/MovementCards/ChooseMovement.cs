using Unity.XR.CoreUtils;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseMovement : MonoBehaviour
{
    [SerializeField] private GameObject gorilla;

    private bool HasChosen;
    private bool DisableUpdate = false;
    private string ChosenMovement;

    GameObject XrOrigin;

    private void Awake()
    {
        XrOrigin = FindFirstObjectByType<XROrigin>().gameObject;
    }

    private void Update()
    {
        if (HasChosen && !DisableUpdate)
        {
            DisableUpdate = true;
            AbilityManager.Instance.HasChosen = true;
            AbilityManager.Instance.ChosenMovement = ChosenMovement;
            XrOrigin.GetComponentInChildren<StaminaBar>().enabled = true;
            this.gameObject.SetActive(false);
            GameObject.Find("ChooseGrab").gameObject.SetActive(false);
        }
    }

    public void ActivateGorilla()
    {
        GameObject Player = Instantiate(gorilla, FindFirstObjectByType<XROrigin>().transform);
        Player.transform.parent = null;
        XrOrigin.SetActive(false);
        ChosenMovement = "gorilla";
        HasChosen = true;
    }

    public void ActivateDash()
    {
        HasChosen = true;
    }

    public void ActivateRockets()
    {
        XrOrigin.GetComponent<DashToDirection>().enabled = true;
        ChosenMovement = "XrOrigin";
        HasChosen = true;
    }
}
