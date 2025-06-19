using UnityEngine;

public class DashToDirection : MonoBehaviour, IPlayerInput, IGunGetState
{
    [Header("Dependencys")]
    public GameObject CubeRight;
    public GameObject CubeLeft;
    private Rigidbody playerRidgidbody;

    [Header("Audio")]
    public AudioClip ThrusterSound;
    public AudioSource AudioSource;

    [Header("Booster Settings")]
    public float ThrustPower;

    [Header("Input")]
    private bool IsRightTriggerPressed;
    private bool IsLeftTriggerPressed;


    private bool isGrounded;

    private bool isGunHeld = false;


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
        playerRidgidbody = GetComponent<Rigidbody>();
        GetInput();
    }

    private void FixedUpdate()
    {
        ThrustPower = AbilityManager.Instance.BoosterSpeed;

        GetGun();

        if (IsRightTriggerPressed)
        {
            AddForceToCubeRightDirection();
        }
        else if (!IsRightTriggerPressed && AudioSource.isPlaying)
        {
            StopNoise();
        }

        if (IsLeftTriggerPressed)
        {
            AddForceToCubeLeftDirection();

        }
        else if (!IsLeftTriggerPressed && AudioSource.isPlaying)
        {
            StopNoise();
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

            playerRidgidbody.AddForce(CubeRight.transform.forward * ThrustPower);
        }

    }
    void AddForceToCubeLeftDirection()
    {

        if (!isGunHeld)
        {
            if (!AudioSource.isPlaying)
                AudioSource.PlayOneShot(ThrusterSound);
            playerRidgidbody.AddForce(CubeLeft.transform.forward * ThrustPower);
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
        if (playerRidgidbody.linearVelocity.x >= 3 || playerRidgidbody.linearVelocity.x <= -3 || playerRidgidbody.linearVelocity.z >= 3 || playerRidgidbody.linearVelocity.z <= -3 && isGrounded)
        {
            playerRidgidbody.AddForce(-playerRidgidbody.linearVelocity.x, 0, -playerRidgidbody.linearVelocity.z);
            playerRidgidbody.linearVelocity = playerRidgidbody.linearVelocity / 2;

        }
    }

    private void GetGun()
    {
        GameObject.FindWithTag("Gun").GetComponent<GunSubject>().AddObserver(this);
    }

    private void GetInput()
    {
        GameObject CurrentInput;

        CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);

    }
}
