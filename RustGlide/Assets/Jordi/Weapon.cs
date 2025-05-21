using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class Weapon : GunSubject
{
    public bool gunHeld = false;

    [SerializeField] protected GameObject Bullethole;
    [SerializeField] protected AudioClip GunShotAudio;
    [SerializeField] protected AudioClip GunHitAudio;
    [SerializeField] protected AudioClip EnemyHitAudio;
    [SerializeField] protected GameObject hitAudioObject;
    [SerializeField] protected float spreadFactor = 0.1f;
    [SerializeField] protected int numberOfProjectiles = 10;
    [SerializeField] protected int Range;

    public InputAction Trigger;
    public ProTubeSettings gunHaptic;

    protected bool cooldownCoroutineRunning = false;
    protected int HandsHeld = 0;
    protected int dmg = 10;
    protected AudioSource gunShotSource;
    protected AudioSource gunHitSource;
    protected ParticleSystem gunHitparticle;
    protected ParticleSystem GunShotParticle;
    protected float cooldown = 0f;
    protected bool onCooldown = false;

    protected void Shoot()
    {
        if (Trigger.ReadValue<float>() > 0.1f && gunHeld && !onCooldown)
        {
            onCooldown = true;
            GunShotParticle.Play();
            gunShotSource.PlayOneShot(GunShotAudio);
            ForceTubeVRInterface.Shoot(gunHaptic);
            RaycastHit hit;
            if (Physics.Raycast(Bullethole.transform.position, Bullethole.transform.TransformDirection(Vector3.forward), out hit, Range))
            {
                GameObject SpawnedObject = Instantiate(hitAudioObject);
                SpawnedObject.transform.parent = null;
                SpawnedObject.transform.position = hit.point;
                gunHitSource = SpawnedObject.GetComponent<AudioSource>();
                gunHitparticle = SpawnedObject.GetComponentInChildren<ParticleSystem>();
                if (hit.collider.CompareTag("Enemy"))
                {
                    StateController stateController = hit.collider.GetComponent<StateController>();
                    stateController.ChangeState(stateController.HurtState);
                    gunHitSource.clip = EnemyHitAudio;
                }
                else
                {
                    gunHitSource.clip = GunHitAudio;
                }
                gunHitparticle.Play();
                gunHitSource.PlayOneShot(gunHitSource.clip);
                StartCoroutine(DeleteSource(SpawnedObject));
            }
        }
    }

    protected void ShotgunShoot()
    {
        if (Trigger.ReadValue<float>() > 0.1f && gunHeld && !onCooldown)
        {
            onCooldown = true;
            GunShotParticle.Play();
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
                    gunHitparticle = SpawnedObject.GetComponentInChildren<ParticleSystem>();
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        StateController stateController = hit.collider.GetComponent<StateController>();
                        stateController.ChangeState(stateController.HurtState);
                        gunHitSource.clip = EnemyHitAudio;
                    }
                    else
                    {
                        gunHitSource.clip = GunHitAudio;
                    }
                    gunHitparticle.Play();
                    gunHitSource.PlayOneShot(gunHitSource.clip);
                    StartCoroutine(DeleteSource(SpawnedObject));
                }
            }
        }
    }

    public IEnumerator SetCooldown()
    {
        cooldownCoroutineRunning = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
        cooldownCoroutineRunning = false;
    }

    public IEnumerator DeleteSource(GameObject gameObject)
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject.gameObject);
    }

    public void OnGrab()
    {
        HandsHeld++;
        if(HandsHeld > 0)
        {
            gunHeld = true;
            GetComponent<Rigidbody>().isKinematic = false;
            NotifyIsGrabbed(gunHeld);
        }
    }

    public void OnRelease()
    {
        HandsHeld--;
        if (HandsHeld == 0)
        {
            gunHeld = false;
            GetComponent<Rigidbody>().isKinematic = true;
            NotifyIsGrabbed(gunHeld);
        }
    }
}
