using Unity.XR.CoreUtils;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private int staminaLossSpeed;
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
        CheckVelocity();
        CalculateVelocity();
    }

    private void CheckVelocity()
    {
        if (stamina >= 0)
        {
            if (velocity <= 5 && velocity > 0)
            {
                staminaLossSpeed = 6 - Mathf.CeilToInt(velocity);
            }
            stamina -= staminaLossSpeed / 15f;
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