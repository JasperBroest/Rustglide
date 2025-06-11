using UnityEngine;

public class Shotgun : Weapon, IPlayerInput
{
    [Range(0, 1)][SerializeField] private float spreadFactor = 0.1f;
    [Range(0, 1000)][SerializeField] private int numberOfProjectiles = 10;
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
            ShotgunShoot();
        }
        else if (!cooldownCoroutineRunning)
        {
            StartCoroutine(SetCooldown());
        }
    }

    private void ShotgunShoot()
    {
        if (LGripPressed && LTriggerPressed && gunHeld && !onCooldown || RGripPressed && RTriggerPressed && gunHeld && !onCooldown)
        {
            onCooldown = true;
            if (Animator != null)
            {
                Animator.SetBool("Shooting", true);
            }
            gunShotParticle.Play();
            gunShotSource.PlayOneShot(GunShotAudio);
            ForceTubeVRInterface.Shoot(gunHaptic);
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
                        //StateController stateController = hit.collider.GetComponent<StateController>();
                        //stateController.ChangeState(stateController.HurtState);
                        hit.collider.GetComponent<EnemyHealth>().TakeDamage(dmg);
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
    }

    private void GetInput()
    {
        GameObject CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }
}
