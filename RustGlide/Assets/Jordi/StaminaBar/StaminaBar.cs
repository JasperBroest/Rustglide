using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private float staminaLossSpeed;

    public bool CanPlayerDie = true;
    public bool IsPlayerDead = false;
    [Range(0, 100)] public float stamina;

    private int staminaLoss;
    private Vector3 vectorVelocity;
    [SerializeField] private float velocity;
    private XROrigin XrOrigin;
    private Vector3 previousPosition;
    private AudioSource audioSource;
    private Volume volume;

    public void TakeDamage(int damage)
    {
        stamina -= damage;
        CheckForDeath();
    }

    private void Start()
    {
        stamina = 100;
        staminaLossSpeed = StoreStamina.instance.staminaLevelMultiplier;
        XrOrigin = FindFirstObjectByType<XROrigin>();
        audioSource = GetComponent<AudioSource>();
        volume = FindFirstObjectByType<Volume>();
    }

    private void Update()
    {
        staminaLossSpeed = StoreStamina.instance.staminaLevelMultiplier;
        float normalizedStamina = Mathf.InverseLerp(0, 100, stamina);
        volume.weight = 1f - normalizedStamina;
        CheckVelocity();
        CalculateVelocity();
        CheckForDeath();
    }

    private void CheckVelocity()
    {
        if (stamina >= 0)
        {
            if (velocity < 5)
            {
                staminaLoss = 6 - Mathf.CeilToInt(velocity);
                stamina -= (staminaLoss * staminaLossSpeed) / 20f;
            }
            else if (velocity > 4.5)
            {
                stamina += staminaLoss / 20f;
                //{
                //    stamina = 100f;
                //}
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

        velocity = vectorVelocity.magnitude * 2;
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
            FindFirstObjectByType<XROrigin>().gameObject.transform.position = GameObject.Find("EndSpawnPos").transform.position;
            GameObject.Find("HUD manager").GetComponent<HudManager>().StartDeathSequence();
            StartCoroutine(finished());

            //audioSource.Play();
            stamina = 100;
            IsPlayerDead = true;
        }

    }

    //remove later
    public IEnumerator finished()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(0);
    }
}