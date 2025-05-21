using UnityEngine;

public class Smg : Weapon
{
    private void OnEnable()
    {
        Trigger.Enable();
    }

    private void Start()
    {
        gunShotSource = GetComponent<AudioSource>();
        GunShotParticle = GetComponentInChildren<ParticleSystem>();
        cooldown = 0.1f;
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
