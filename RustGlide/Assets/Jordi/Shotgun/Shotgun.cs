using UnityEngine;

public class Shotgun : Weapon
{
    private void OnEnable()
    {
        Trigger.Enable();
    }

    private void Start()
    {
        gunShotSource = GetComponent<AudioSource>();
        GunShotParticle = GetComponentInChildren<ParticleSystem>();
        cooldown = 0.5f;
    }

    private void Update()
    {
        if (!onCooldown)
        {
            ShotgunShoot();
        }
        else if (!cooldownCoroutineRunning)
        {
            StartCoroutine(SetCooldown());
        }
    }
}
