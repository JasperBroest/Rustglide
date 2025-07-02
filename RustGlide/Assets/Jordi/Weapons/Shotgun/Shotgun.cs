using UnityEngine;
using UnityEngine.VFX;

public class Shotgun : Weapon, IPlayerInput
{
    [Range(0, 1000)] public float numberOfProjectiles = 10;

    [SerializeField] private float FireRate;

    private void Start()
    {
        GetInput();
        gunShotSource = GetComponent<AudioSource>();
        gunShotParticle = GetComponentInChildren<VisualEffect>();
        
    }

    private void Update()
    {
        cooldown = AbilityManager.Instance.CurrentShotgunCooldown;
        
        if (gunHeld)
            dmg = AbilityManager.Instance.CurrentShotgunDamage;
        
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
