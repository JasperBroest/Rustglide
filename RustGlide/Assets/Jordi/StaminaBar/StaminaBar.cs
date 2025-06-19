using System.Collections;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering;

using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class StaminaBar : MonoBehaviour
{
    // Velocity
    [SerializeField] private float velocity;
    [SerializeField][Range(0, 10)] private float velocitySpeed;

    // Stamina
    [Range(0, 100)] public float stamina;
    private int staminaLoss;

    // Other
    private Vector3 previousPosition;
    private XROrigin XrOrigin;
    private Volume volume;
    private Vignette vignette;

    public void TakeDamage(int damage)
    {
        stamina -= damage;
        CheckVelocity();
    }

    private void Awake()
    {
        XrOrigin = FindFirstObjectByType<XROrigin>();
        volume = FindFirstObjectByType<Volume>();
    }

    private void Update()
    {
        if (volume.profile.TryGet(out vignette))
        {
            float normalizedStamina = Mathf.InverseLerp(0, 100, stamina);
            if (stamina <= 50)
            {
                // Smoothly interpolate the vignette intensity towards the target value
                float targetIntensity = 1f - normalizedStamina;
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetIntensity, Time.deltaTime * 3f);
            }
        }

        CheckVelocity();
        CalculateVelocity();
    }

    private void CheckVelocity()
    {
        if (stamina >= 0)
        {
            // Calculates stamina decrease based on how fast player is moving
            if (velocity < velocitySpeed)
            {
                staminaLoss = 6 - Mathf.CeilToInt(velocity);
                float lossSpeed = staminaLoss / 20f;
                stamina -= lossSpeed;
            }

            // Dont go over 100
            else if (stamina <= 100)
            {
                stamina += staminaLoss / 20f;
            }
        }
        else
        {
            Die();
        }
    }

    private void CalculateVelocity()
    {
        Vector3 currentPosition;
        currentPosition = XrOrigin.transform.position;

        Vector3 vectorVelocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;

        velocity = vectorVelocity.magnitude * 2f;
    }
    private void Die()
    {
        StartCoroutine(finished());
        vignette.center.value = new Vector2(-1, -1);

    }

    //remove later
    public IEnumerator finished()
    {
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(0);
    }
}