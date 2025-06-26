using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class DashToDirection : MonoBehaviour, IPlayerInput, IGunGetState
{
    [Header("Dependencys")] public GameObject CubeRight;
    public GameObject CubeLeft;
    
    public ParticleSystem SpeedParticles;
    
    private Rigidbody playerRidgidbody;
    

    [Header("Audio")] public AudioClip ThrusterSound;
    public AudioSource AudioSource;

    private float ThrustPower;

    [Header("Input")] public bool IsRightTriggerPressed;
    public bool IsLeftTriggerPressed;

    [Header("activatedHand")]
    public bool rightBoosterActivated = true;
    public bool leftBoosterActivated = true;

    private GameObject leftHand;
    private GameObject rightHand;

    private bool isGrounded;

    public bool isGunHeld = false;

    private GameObject[] guns;


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

    public void RightGrip(bool RGrip)
    {
        
    }

    public void LeftGrip(bool RGrip)
    {

    }

    private void Awake()
    {
        leftHand = GameObject.Find("Left Controller");
        rightHand = GameObject.Find("Right Controller");
    }

    private void Start()
    {
        AudioSource.clip = ThrusterSound;
        playerRidgidbody = GetComponent<Rigidbody>();
        GetInput();
        GetGun();
    }

    private void FixedUpdate()
    {
        if(playerRidgidbody.linearVelocity.magnitude > 5) SpeedParticles.Play();
            else if(playerRidgidbody.linearVelocity.magnitude < 5)SpeedParticles.Stop();
        
        Vector3 velocity = playerRidgidbody.linearVelocity;

        if (velocity.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation( -velocity.normalized);
            SpeedParticles.transform.rotation = rotation;
        }


        ThrustPower = AbilityManager.Instance.BoosterSpeed;

        if (IsRightTriggerPressed && rightBoosterActivated)
        {
            AddForceToCubeRightDirection();
            rightHand.GetComponentInChildren<ParticleSystem>().Play();
        }
        else if (!IsRightTriggerPressed)
        {
            rightHand.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (IsLeftTriggerPressed && leftBoosterActivated)
        {
            AddForceToCubeLeftDirection();
            leftHand.GetComponentInChildren<ParticleSystem>().Play();
        }
        else if (!IsLeftTriggerPressed)
        {
            leftHand.GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (!IsLeftTriggerPressed && isGrounded)
        {
            CheckForSlow();
        }

        if(!IsLeftTriggerPressed && !IsRightTriggerPressed && AudioSource.isPlaying)
        {
            StopNoise();
        }
    }

    void AddForceToCubeRightDirection()
    {
        if (!AudioSource.isPlaying)
            AudioSource.PlayOneShot(ThrusterSound);

        playerRidgidbody.AddForce(CubeRight.transform.forward * ThrustPower);
    }

    void AddForceToCubeLeftDirection()
    {
        if (!AudioSource.isPlaying)
            AudioSource.PlayOneShot(ThrusterSound);
        playerRidgidbody.AddForce(CubeLeft.transform.forward * ThrustPower);
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
        if (playerRidgidbody.linearVelocity.x >= 3 || playerRidgidbody.linearVelocity.x <= -3 ||
            playerRidgidbody.linearVelocity.z >= 3 || playerRidgidbody.linearVelocity.z <= -3 && isGrounded)
        {
            playerRidgidbody.AddForce(-playerRidgidbody.linearVelocity.x, 0, -playerRidgidbody.linearVelocity.z);
            playerRidgidbody.linearVelocity = playerRidgidbody.linearVelocity / 2;
        }
    }

    private void GetGun()
    {
        if (guns == null)
            guns = GameObject.FindGameObjectsWithTag("Gun");

        foreach (GameObject gun in guns)
        {
            gun.GetComponent<GunSubject>().AddObserver(this);
        }
    }

    private void GetInput()
    {
        GameObject CurrentInput;

        CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }
}