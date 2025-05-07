using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private float stamina;
    [SerializeField] private float velocity;
    [SerializeField] private float staminaLossSpeed;

    private void Start()
    {
        stamina = 100;
        staminaLossSpeed = 1;
        velocity = 6;
    }

    private void Update()
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

        if (velocity > 5 && stamina < 100)
        {
            stamina += velocity * stamina / staminaLossSpeed / 100;
        }
    }
}