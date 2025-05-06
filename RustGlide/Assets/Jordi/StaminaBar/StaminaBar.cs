using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField]private float stamina;
    [SerializeField]private float velocity;
    [SerializeField]private float staminaLossSpeed;

    private void Start()
    {
        stamina = 100;
        staminaLossSpeed = 1;
        velocity = 6;
    }

    private void Update()
    {
        if(velocity <= 5)
        {
            stamina -= staminaLossSpeed;
        }
        else if(stamina < 100)
        {
           stamina += velocity * stamina / staminaLossSpeed;
        }
    }
}
