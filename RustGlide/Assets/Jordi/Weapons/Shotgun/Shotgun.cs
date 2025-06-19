using UnityEngine;
using UnityEngine.VFX;

public class Shotgun : Weapon, IPlayerInput
{
    [Range(0, 1)][SerializeField] private float spreadFactor = 0.1f;
    [Range(0, 1000)][SerializeField] private int numberOfProjectiles = 10;
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
            ShotgunShoot();
        }
        else if (!cooldownCoroutineRunning)
        {
            StartCoroutine(SetCooldown());
        }
    }

    protected void ShotgunShoot()
    {
        if (!gunHeld) return;

        if (gunHoldingHand == Hand.Left && LTriggerPressed && !onCooldown)
        {
            onCooldown = true;
            FireGun();
        }
        else if (gunHoldingHand == Hand.Right && RTriggerPressed && !onCooldown)
        {
            onCooldown = true;
            FireGun();
        }
    }


    private void FireGun()
    {
        if (Animator != null)
        {
            Animator.SetBool("Shooting", true);
        }
        gunShotParticle.Play();
        gunShotSource.PlayOneShot(GunShotAudio);
        ForceTubeVRInterface.Shoot(gunHaptic);

        //shoots a raycast out of the bullethole of the gun and spawns a particle and sound effect on the place you hit
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 direction = Bullethole.transform.forward;
            direction += Bullethole.transform.up * Random.Range(-spreadFactor, spreadFactor);
            direction += Bullethole.transform.right * Random.Range(-spreadFactor, spreadFactor);

            RaycastHit hit;
            if (Physics.Raycast(Bullethole.transform.position, direction.normalized, out hit, Range))
            {
                GameObject SpawnedObject = Instantiate(hitAudioObject);
                SpawnedObject.transform.parent = null;
                SpawnedObject.transform.position = hit.point;
                gunHitSource = SpawnedObject.GetComponent<AudioSource>();
                gunHitParticle = SpawnedObject.GetComponentInChildren<ParticleSystem>();
                if (hit.collider.CompareTag("Enemy"))
                {
                    StateController stateController = hit.collider.GetComponent<StateController>();
                    stateController.DamageTaken = dmg;
                    stateController.ChangeState(stateController.hurtState);
                    hit.collider.GetComponent<StateController>().ChangeState(stateController.hurtState);
                    gunHitSource.clip = EnemyHitAudio;
                }
                else
                {
                    gunHitSource.clip = GunHitAudio;
                }
                gunHitParticle.Play();
                gunHitSource.PlayOneShot(gunHitSource.clip);
                StartCoroutine(DeleteSource(SpawnedObject));
            }
        }
    }
    private void GetInput()
    {
        GameObject CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }
}
