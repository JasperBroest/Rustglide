using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering;

using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class StaminaBar : MonoBehaviour
{
    [SerializeField] private float staminaLossSpeed;

    /*public bool CanPlayerDie = true;*/
    public bool IsPlayerDead = false;
    [Range(0, 100)] public float stamina;

    private int staminaLoss;
    private Vector3 vectorVelocity;
    private float velocitySpeed;
    [SerializeField] private float velocity;
    private XROrigin XrOrigin;
    private Vector3 previousPosition;
    private Volume volume;
    private Vignette vignette;

    public void TakeDamage(int damage)
    {
        stamina -= damage;
        CheckForDeath();
    }

    private void Awake()
    {
        XrOrigin = FindFirstObjectByType<XROrigin>();
        volume = FindFirstObjectByType<Volume>();
    }

    private void Start()
    {
        stamina = 100;
        if (XrOrigin.name == "Gorilla Rig")
        {
            StoreStamina.instance.staminaLevelMultiplier = StoreStamina.instance.staminaLevelMultiplier - 0.3f;
            staminaLossSpeed = StoreStamina.instance.staminaLevelMultiplier;
            velocitySpeed = 4;
        }
        else
        {
            velocitySpeed = 4.5f;
        }
    }

    private void Update()
    {
        staminaLossSpeed = StoreStamina.instance.staminaLevelMultiplier;
        if (volume.profile.TryGet(out vignette))
        {
            float normalizedStamina = Mathf.InverseLerp(0, 100, stamina);
            vignette.intensity.value = 1f - normalizedStamina;
        }
        CheckVelocity();
        CalculateVelocity();
        CheckForDeath();
    }

    private void CheckVelocity()
    {
        if (stamina >= 0)
        {
            if (velocity < velocitySpeed)
            {
                staminaLoss = 6 - Mathf.CeilToInt(velocity);
                stamina -= (staminaLoss * staminaLossSpeed) / 20f;
            }
            else if (stamina <= 100)
            {
                stamina += staminaLoss / 20f;
            }
        }
    }

    private void CalculateVelocity()
    {
        Vector3 currentPosition;
        if (XrOrigin.name == "Gorilla Rig")
        {
            currentPosition = XrOrigin.transform.GetChild(2).position;
        }
        else
        {
            currentPosition = XrOrigin.transform.position;
        }
        vectorVelocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;

        velocity = vectorVelocity.magnitude * 2f;
    }

    private void CheckForDeath()
    {
        if (stamina <= 1)
        {
            Die();
        }
    }
    private void Die()
    {
        if (!IsPlayerDead)
        {
            GameObject.Find("HUD manager").GetComponent<HudManager>().StartDeathSequence();
            StartCoroutine(finished());
            vignette.center.value = new Vector2(-1, -1);
            IsPlayerDead = true;
        }

    }

    //remove later
    public IEnumerator finished()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}