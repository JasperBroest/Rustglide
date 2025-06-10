using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashMovement : MonoBehaviour, IPlayerInput
{
    [Header("Coolcown")]
    public float CoolDownDuration;
    public bool IsOnCooldown;


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
        Debug.Log(RState + "Right");
    }
    public void LeftTrigger(bool LState)
    {
        IsLeftTriggerPressed = LState;
        Debug.Log(LState + "Left");
    }

    private void Start()
    {
        AudioSource.clip = ThrusterSound;
        /* GetGun();*/
        GetInput();
    }

    void Update()
    {
        if (IsRightTriggerPressed)
        {
            
            AddForceToCubeRightDirection();
        }
       

        if (IsLeftTriggerPressed)
        {
            AddForceToCubeLeftDirection();

        }
        
    }

    void AddForceToCubeRightDirection()
    {
       
            if (!AudioSource.isPlaying)
                AudioSource.PlayOneShot(ThrusterSound);
        if (!IsOnCooldown)
            PlayerRigidbody.AddForce(CubeRight.transform.forward * ThrustPower);
    }
    void AddForceToCubeLeftDirection()
    {

        if (!isGunHeld)
        {
            if (!AudioSource.isPlaying)
                AudioSource.PlayOneShot(ThrusterSound);
            if (!IsOnCooldown)
                PlayerRigidbody.AddForce(CubeLeft.transform.forward * ThrustPower);
        }

    }

    void StopNoise()
    {
        AudioSource.Stop();
    }

   

    private void GetInput()
    {
        GameObject CurrentInput;

        CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);

    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CoolDownDuration);
        IsOnCooldown = true;
    }
}
