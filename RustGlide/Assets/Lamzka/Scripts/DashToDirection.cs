using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashToDirection : MonoBehaviour
{
    public GameObject CubeRight;
    public GameObject CubeLeft;

    public Rigidbody PlayerRigidbody;

    public InputAction RightTrigger;
    public InputAction LeftTrigger;

    public AudioClip ThrusterSound;
    public AudioSource AudioSource;

    public float ThrustPower;

    private bool HasRightBeenPressed;
    private bool HasLeftBeenPressed;
    private bool isGrounded;

    void OnEnable()
    {
        RightTrigger.Enable();
        LeftTrigger.Enable();
    }

    private void Start()
    {
        AudioSource.clip = ThrusterSound;
    }

    void Update()
    {
        //Debug.Log(PlayerRigidbody.linearVelocity);
        if (RightTrigger.ReadValue<float>() > 0.1f)
        {
            AddForceToCubeRightDirection();
            HasRightBeenPressed = true;
        }
        else if (HasRightBeenPressed && AudioSource.isPlaying)
        {
            StopNoise();
            HasRightBeenPressed = false;
        }

        if (LeftTrigger.ReadValue<float>() > 0.1f)
        {
            AddForceToCubeLeftDirection();
            HasLeftBeenPressed = true;
        }
        else if (HasLeftBeenPressed && AudioSource.isPlaying)
        {
            StopNoise();
            HasLeftBeenPressed = false;
        }
        if (LeftTrigger.WasReleasedThisFrame())
        {
            Debug.Log("Slow");
        }
    }

    void AddForceToCubeRightDirection()
    {
        if (!AudioSource.isPlaying)
            AudioSource.PlayOneShot(ThrusterSound);
        PlayerRigidbody.AddForce(CubeRight.transform.forward * ThrustPower);
    }
    void AddForceToCubeLeftDirection()
    {
        if (!AudioSource.isPlaying)
            AudioSource.PlayOneShot(ThrusterSound);
        PlayerRigidbody.AddForce(CubeLeft.transform.forward * ThrustPower);
    }

    void StopNoise()
    {
        AudioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void CheckForSlow(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (PlayerRigidbody.linearVelocity.x >= 5 || PlayerRigidbody.linearVelocity.z >= 5)
            {
                Debug.Log("SPED");
                //PlayerRigidbody.linearVelocity = Vector3.zero;
            }
        }
    }
}
