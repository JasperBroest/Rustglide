using UnityEngine;

public class Smg : Weapon
{
    [SerializeField] private int FireRate;

    private void OnEnable()
    {
        Trigger.Enable();
    }

    private void Start()
    {
        gunShotSource = GetComponent<AudioSource>();
        GunShotParticle = GetComponentInChildren<ParticleSystem>();
        cooldown = 1f / FireRate;
    }

    private void Update()
    {
        if (!onCooldown)
        {
            Shoot();
        }
        else if (!cooldownCoroutineRunning)
        {
            StartCoroutine(SetCooldown());
        }
    }
}
