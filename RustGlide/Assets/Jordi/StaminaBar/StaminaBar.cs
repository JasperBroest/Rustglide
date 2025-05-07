using Unity.XR.CoreUtils;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private float stamina;
    private Vector3 vectorVelocity;
    private float velocity;
    [SerializeField] private float staminaLossSpeed;

    [SerializeField] private GameObject XrOrigin;
    private Rigidbody rb; Vector3 previousPosition;

    private void Start()
    {
        stamina = 100;
        staminaLossSpeed = 1;
        rb = XrOrigin.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckVelocity();
        calculateVelocity();
        Debug.Log(velocity);
    }

    private void CheckVelocity()
    {
        if (stamina >= 0)
        {
            if (velocity <= 5 && velocity > 4)
            {
                staminaLossSpeed = 1;
                stamina -= staminaLossSpeed / 20;
            }
            else if (velocity <= 4 && velocity > 3)
            {
                staminaLossSpeed = 2;
                stamina -= staminaLossSpeed / 20;
            }
            else if (velocity <= 3 && velocity > 2)
            {
                staminaLossSpeed = 3;
                stamina -= staminaLossSpeed / 20;
            }
            else if (velocity <= 2 && velocity > 1)
            {
                staminaLossSpeed = 4;
                stamina -= staminaLossSpeed / 20;
            }
            else if (velocity <= 1)
            {
                staminaLossSpeed = 5;
                stamina -= staminaLossSpeed / 20;
            }
        }
    }

    private void calculateVelocity()
    {
        Vector3 currentPosition = XrOrigin.transform.position;
        vectorVelocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;

        velocity = vectorVelocity.magnitude * 2;
    }
}