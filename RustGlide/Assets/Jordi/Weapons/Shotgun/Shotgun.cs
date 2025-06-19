using UnityEngine;
using UnityEngine.VFX;

public class Shotgun : Weapon, IPlayerInput
{
    [Range(0, 1)] public float spreadFactor = 0.1f;
    [Range(0, 1000)] public int numberOfProjectiles = 10;

    [SerializeField] private int FireRate;

    private void Start()
    {
        GetInput();
        gunShotSource = GetComponent<AudioSource>();
        gunShotParticle = GetComponentInChildren<VisualEffect>();
        cooldown = AbilityManager.Instance.CurrentShotgunCooldown / FireRate;
    }

    private void Update()
    {
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
