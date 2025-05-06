using UnityEngine;
using UnityEngine.InputSystem;

public class DashToDirection : MonoBehaviour
{
    public GameObject CubeRight;
    public GameObject CubeLeft;

    public Rigidbody PlayerRigidbody;

    public InputAction RightTrigger;
    public InputAction LeftTrigger;

    public float ThrustPower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        RightTrigger.Enable();
        LeftTrigger.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (RightTrigger.ReadValue<float>() > 0.1f)
        {

            Debug.Log("it works");

            AddForceToCubeDirection();
        }
    }

    void AddForceToCubeDirection()
    {
        PlayerRigidbody.AddForce(CubeRight.transform.forward * ThrustPower);
    }
}
