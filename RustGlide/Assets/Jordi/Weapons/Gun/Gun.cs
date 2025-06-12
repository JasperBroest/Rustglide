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
        Shoot();
        if (LGripPressed && !LTriggerPressed || RGripPressed && !RTriggerPressed)
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
