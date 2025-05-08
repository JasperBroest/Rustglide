using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public StaminaBar StaminaBarScript;

    public Slider StaminaSlider;



    void Update()
    {
        StaminaSlider.value = StaminaBarScript.stamina;
    }
}
