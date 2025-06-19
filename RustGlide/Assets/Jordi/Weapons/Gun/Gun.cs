using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Gun : Weapon, IPlayerInput
{
    private void Start()
    {
        GetInput();
        gunShotSource = GetComponent<AudioSource>();
        gunShotParticle = GetComponentInChildren<VisualEffect>();
        gunShotSource.clip = GunShotAudio;
    }

    private void Update()
    {

        if (gunHeld)
            dmg = AbilityManager.Instance.CurrentHandGunDamage;
        
        GetInput();
        Shoot();
        if (gunHoldingHand == Hand.Left && !LTriggerPressed && onCooldown || gunHoldingHand == Hand.Right && !RTriggerPressed && onCooldown)
        {
            Animator.SetBool("Shooting", false);
            onCooldown = false;
        }
    }

    private void GetInput()
    {
        GameObject CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }
}
