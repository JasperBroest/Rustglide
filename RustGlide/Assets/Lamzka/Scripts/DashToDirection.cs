using UnityEngine;

public class DashToDirection : MonoBehaviour, IPlayerInput, IGunGetState
{
    [Header("dependencys")]
    public GameObject CubeRight;
    public GameObject CubeLeft;
    public Rigidbody PlayerRigidbody;

    [Header("Audio")]
    public AudioClip ThrusterSound;
    public AudioSource AudioSource;

    [Header("Booster Settings")]
    public float ThrustPower;

    [Header("Input")]
    private bool IsRightTriggerPressed;
    private bool IsLeftTriggerPressed;

    private bool HasRightBeenPressed;
    private bool HasLeftBeenPressed;
    private bool isGrounded;

    private bool isGunHeld;

    bool nospeed = false;



    public void NotifyGrab(bool IsGunGrabbed)
    {
        isGunHeld = IsGunGrabbed;
    }

    public void RightTrigger(bool RState)
    {
        IsRightTriggerPressed = RState;
    }
    public void LeftTrigger(bool LState)
    {
        IsLeftTriggerPressed = LState;
    }

    private void Start()
    {
        AudioSource.clip = ThrusterSound;
        GetGun();
    }

    void Update()
    {
        if (IsRightTriggerPressed)
        {
            AddForceToCubeRightDirection();
            HasRightBeenPressed = true;
        }
        else if (HasRightBeenPressed && AudioSource.isPlaying)
        {
            StopNoise();
            HasRightBeenPressed = false;
        }

        if (IsLeftTriggerPressed)
        {
            AddForceToCubeLeftDirection();
            HasLeftBeenPressed = true;
        }
        else if (HasLeftBeenPressed && AudioSource.isPlaying)
        {
            StopNoise();
            HasLeftBeenPressed = false;
        }
        if (!IsLeftTriggerPressed && isGrounded)
        {
            CheckForSlow();
        }
    }

    void AddForceToCubeRightDirection()
    {
        if (!isGunHeld)
        {
            if (!AudioSource.isPlaying)
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
