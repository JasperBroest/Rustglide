using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public InputAction Trigger;

    [SerializeField]private GameObject Bullethole;
    private int Dmg;

    private void OnEnable()
    {
        Trigger.Enable();
    }

    private void Update()
    {
        if(Trigger.ReadValue<float>() > 0.1f)
        {
            RaycastHit hit;
            if (Physics.Raycast(Bullethole.transform.position, Bullethole.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    //get hp and decrease it with Dmg
                    Debug.Log("hit");
                }
            }
        }
    }
}
