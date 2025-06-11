using UnityEngine;

public class Smg : Weapon, IPlayerInput
{
    [SerializeField] private int FireRate;

    private void Start()
    {
        GetInput();
        gunShotSource = GetComponent<AudioSource>();
        gunShotParticle = GetComponentInChildren<ParticleSystem>();
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

    private void GetInput()
    {
        GameObject CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }
}
