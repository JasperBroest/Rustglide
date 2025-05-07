using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]private GameObject Bullethole;
    private int Dmg;

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Bullethole.transform.position, Bullethole.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                //get hp and decrease it with Dmg
                Debug.Log("hit");
            }
        }
    }
}
