using Unity.XR.CoreUtils;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private float staminaLossSpeed;
    [SerializeField] private GameObject playerSpawn;

    public float stamina;
    private Vector3 vectorVelocity;
    private float velocity;

    private XROrigin XrOrigin;
    Vector3 previousPosition;

    public void TakeDamage(int damage)
    {
        stamina -= damage;
        if (stamina <= 0)
        {
            Die();
        }
    }

    private void Start()
    {
        stamina = 100;

        XrOrigin = FindFirstObjectByType<XROrigin>();
    }

    private void Update()
    {
        //CheckVelocity();
        CalculateVelocity();
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

    private void CalculateVelocity()
    {
        Vector3 currentPosition = XrOrigin.transform.position;
        vectorVelocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;

        velocity = vectorVelocity.magnitude * 2;
    }

    private void Die()
    {
        transform.position = playerSpawn.transform.position;
        stamina = 100;
    }

}