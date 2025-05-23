using Unity.XR.CoreUtils;
using UnityEngine;
using System.Collections;

public class ChooseMovement : MonoBehaviour
{
    [SerializeField] private GameObject gorilla;

    GameObject XrOrigin;

    private void Awake()
    {
        XrOrigin = FindFirstObjectByType<XROrigin>().gameObject;
    }

    public void ActivateGorilla()
    {
        GameObject Player = Instantiate(gorilla, FindFirstObjectByType<XROrigin>().transform);
        Player.transform.parent = null;
        XrOrigin.SetActive(false);
        this.gameObject.SetActive(false);
        GameObject.Find("ChooseGrab").gameObject.SetActive(false);
    }

    public void ActivateDash()
    {
        XrOrigin.GetComponent<DashMovement>().enabled = true;
        XrOrigin.GetComponentInChildren<StaminaBar>().enabled = true;
        this.gameObject.SetActive(false);
        GameObject.Find("ChooseGrab").gameObject.SetActive(false);
    }

    public void ActivateRockets()
    {
        XrOrigin.GetComponent<DashToDirection>().enabled = true;
        XrOrigin.GetComponentInChildren<StaminaBar>().enabled = true;
        this.gameObject.SetActive(false);
        GameObject.Find("ChooseGrab").gameObject.SetActive(false);
    }
}
