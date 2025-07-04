using System;
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
    public bool staminaOn = false;
    [Range(0, 1000)] public float stamina;
    private int staminaLoss;

    public AudioClip HurtSound;
    public AudioClip DeathSound; 
    
    public AudioSource HeartBeataudioSource;
        
    
    
    // Other
    private Vector3 previousPosition;
    private XROrigin XrOrigin;
    private Volume volume;
    private Vignette vignette;
    private AudioSource audioSource;
    GameObject chooseWeapon;

    
    bool hasDied;
    

    public void TakeDamage(int damage)
    {
        stamina -= damage;
        audioSource.PlayOneShot(HurtSound);
        CheckVelocity();
    }

    private void Awake()
    {
        XrOrigin = FindFirstObjectByType<XROrigin>();
        volume = FindFirstObjectByType<Volume>();
        audioSource = GetComponent<AudioSource>();

        stamina = AbilityManager.Instance.Stamina;

        chooseWeapon = GameObject.Find("ChooseWeapon");
    }

    private void Start()
    {

        hasDied = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ChangeWindVolumeOnvelocity();
        
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

        if(chooseWeapon != null)
        {
            if(chooseWeapon.GetComponent<RogueLikeManager>().HasChosen)
            {
                staminaOn = true;
            }
        }

        if(staminaOn)
        {
            CheckVelocity();
            CalculateVelocity();
        }
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

            // Dont go over stamina 
            else if (stamina <= AbilityManager.Instance.Stamina)
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
        if (!hasDied)
        {
            audioSource.PlayOneShot(DeathSound);
        }
        hasDied = true;

        AbilityManager.Instance.ResetStats();
        StartCoroutine(finished());
        vignette.color.value = Color.black;
        vignette.center.value = new Vector2(-1, -1);
        
        

    }
    
    
    void ChangeWindVolumeOnvelocity()
    {
        if (stamina <= 50f)
        {
            float max = 0.650f;
        
            float currentVolume = Mathf.Clamp01(stamina / 15) * max;
            HeartBeataudioSource.volume = currentVolume;
            
            if (!HeartBeataudioSource.isPlaying)
            {
                HeartBeataudioSource.Play();
            }
            
        }
        else if (stamina >= 50f)
        {
            HeartBeataudioSource.Stop();
        }
            
        
        
        
    }

    //remove later
    public IEnumerator finished()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}