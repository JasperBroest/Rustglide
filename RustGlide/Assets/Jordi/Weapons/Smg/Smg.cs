using UnityEngine;
using UnityEngine.VFX;

public class Smg : Weapon, IPlayerInput
{
    [SerializeField] private int FireRate;

    private void Start()
    {
        GetInput();
        gunShotSource = GetComponent<AudioSource>();
        gunShotParticle = GetComponentInChildren<VisualEffect>();
        cooldown = AbilityManager.Instance.CurrentSMGShootingCooldown / FireRate;
    }

    private void Update()
    {
        dmg = AbilityManager.Instance.CurrentSMGDamage;
        cooldown = AbilityManager.Instance.DefaultSMGShootingCooldown;

        if (gunHeld)
            dmg = AbilityManager.Instance.CurrentSMGDamage;
        
        GetInput();
        if (!onCooldown)
        {
            Shoot();
        }
        else if (!cooldownCoroutineRunning)
        {
            StartCoroutine(SetCooldown());
        }
    }

    private void GetInput()
    {
        GameObject CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }
}
