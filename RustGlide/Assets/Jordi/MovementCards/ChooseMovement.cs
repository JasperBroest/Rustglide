using Unity.XR.CoreUtils;
using UnityEngine;
using System.Collections;

public class ChooseMovement : MonoBehaviour
{
    [SerializeField] private GameObject gorilla;
    [SerializeField] private GameObject dash;
    [SerializeField] private GameObject rockets;

    private void Awake()
    {
        gorilla = GameObject.FindGameObjectWithTag("GorillaPlayer");
        dash = GameObject.FindGameObjectWithTag("DashPlayer");
        rockets = GameObject.FindGameObjectWithTag("RocketPlayer");
    }

    public void ActivateGorilla()
    {
        gorilla.SetActive(true);
        dash.SetActive(false);
        rockets.SetActive(false);
        FindFirstObjectByType<Chosen>().HasChosen = true;
        this.gameObject.SetActive(false);
    }

    public void ActivateDash()
    {
        gorilla.SetActive(false);
        dash.SetActive(true);
        rockets.SetActive(false);
        FindFirstObjectByType<Chosen>().HasChosen = true;
        this.gameObject.SetActive(false);
    }

    public void ActivateRockets()
    {
        gorilla.SetActive(false);
        dash.SetActive(false);
        rockets.SetActive(true);
        FindFirstObjectByType<Chosen>().HasChosen = true;
        this.gameObject.SetActive(false);
    }
}
