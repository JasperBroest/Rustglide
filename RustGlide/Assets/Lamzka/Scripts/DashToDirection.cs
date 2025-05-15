using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashToDirection : MonoBehaviour, IGunGetState
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

    private bool isGunHeld;

    bool nospeed = false;

    void OnEnable()
    {
        RightTrigger.Enable();
        LeftTrigger.Enable();
    }

    public void NotifyGrab(bool IsGunGrabbed)
    {
        isGunHeld = IsGunGrabbed;
    }

    private void Start()
    {
        AudioSource.clip = ThrusterSound;
        GetGun();
    }

    void Update()
    {
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
        if (LeftTrigger.WasReleasedThisFrame() && isGrounded)
        {
            CheckForSlow();
        }
    }

    void AddForceToCubeRightDirection()
    {
        if (!isGunHeld)
        {
            if(!AudioSource.isPlaying)
            AudioSource.PlayOneShot(ThrusterSound);

            PlayerRigidbody.AddForce(CubeRight.transform.forward * ThrustPower);
        }
           
    }
    void AddForceToCubeLeftDirection()
    {

        if (!isGunHeld)
        {
            if (!AudioSource.isPlaying)
                AudioSource.PlayOneShot(ThrusterSound);
            PlayerRigidbody.AddForce(CubeLeft.transform.forward * ThrustPower);
        }
            
    }

    void StopNoise()
    {
        AudioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            CheckForSlow();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void CheckForSlow()
    {
        if (PlayerRigidbody.linearVelocity.x >= 3 || PlayerRigidbody.linearVelocity.x <= -3 || PlayerRigidbody.linearVelocity.z >= 3 || PlayerRigidbody.linearVelocity.z <= -3 && isGrounded)
        {
            PlayerRigidbody.AddForce(-PlayerRigidbody.linearVelocity.x, 0, -PlayerRigidbody.linearVelocity.z);
            PlayerRigidbody.linearVelocity = PlayerRigidbody.linearVelocity / 2;
            
        }
    }

    private void GetGun()
    {
        GameObject.FindWithTag("Gun").GetComponent<GunSubject>().AddObserver(this);
    }
}
