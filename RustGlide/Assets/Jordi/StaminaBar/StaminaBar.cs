using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private int staminaLossSpeed;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject gunSpawn;

    [Range(0, 100)]
    public float stamina;

    private Vector3 vectorVelocity;
    private float velocity;

    private XROrigin XrOrigin;
    Vector3 previousPosition;

    private AudioSource audioSource;

    public void TakeDamage(int damage)
    {
        stamina -= damage;
        CheckForDeath();
    }

    private void Start()
    {
        stamina = 100;

        XrOrigin = FindFirstObjectByType<XROrigin>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckVelocity();
        CalculateVelocity();
        CheckForDeath();
    }

    private void CheckVelocity()
    {
        if (stamina >= 0)
        {
            if (velocity < 4.5)
            {
                staminaLossSpeed = 6 - Mathf.CeilToInt(velocity);
                stamina -= staminaLossSpeed / 20f;
            }
            else if (velocity > 4.5)
            {
                stamina += staminaLossSpeed / 20f;
                if (stamina > 100f)
                {
                    stamina = 100f;
                }
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

    private void CheckForDeath()
    {
        if (stamina <= 1)
        {
            Die();
        }
    }
    private void Die()
    {
        /*XrOrigin.transform.position = playerSpawn.transform.position;
        gun.transform.position = gunSpawn.transform.position;*/

        GameObject.Find("Player").transform.position = GameObject.Find("EndSpawnPos").transform.position;
        GameObject.Find("HUD manager").GetComponent<HudManager>().StartDeathSequence();
        StartCoroutine(finished());

        audioSource.Play();
        stamina = 100;
    }


    //remove later
    public IEnumerator finished()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(0);
    }
}