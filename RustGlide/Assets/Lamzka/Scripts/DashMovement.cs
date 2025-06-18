using UnityEngine;

public class DashMovement : MonoBehaviour/*, IPlayerInput*/
{
    [Header("Dependencys")]
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
    public void RightGrip(bool RGState)
    {
        
    }

    public void LeftGrip(bool LGState)
    {
        
    }

    private void Start()
    {
        AudioSource.clip = ThrusterSound;
        /*  GetGun();*/
        //GetInput();
    }

    void Update()
    {
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

    /*private void GetGun()
    {
        GameObject.FindWithTag("Gun").GetComponent<GunSubject>().AddObserver(this);
    }*/

    private void GetInput()
    {
        GameObject CurrentInput;

        CurrentInput = GameObject.FindWithTag("PlayerInput");
        //CurrentInput.GetComponent<InputSubject>().AddObserver(this);

    }
}
