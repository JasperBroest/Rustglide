using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon, IPlayerInput
{
    private bool TriggerPressed;

    private void OnEnable()
    {
        Trigger.Enable();
    }

    public void RightTrigger(bool RState)
    {
        TriggerPressed = RState;
    }

    public void LeftTrigger(bool LState)
    {

    }


    private void Start()
    {
        //GetInput();
        gunShotSource = GetComponent<AudioSource>();
        GunShotParticle = GetComponentInChildren<ParticleSystem>();
        gunShotSource.clip = GunShotAudio;
    }

    private void Update()
    {
        Shoot();
        if (Trigger.ReadValue<float>() == 0 && gunHeld)
        {
            Animator.SetBool("Shooting", false);
            onCooldown = false;
        }
    }

    private void GetInput()
    {
        GameObject CurrentInput;

        CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);

    }
}
