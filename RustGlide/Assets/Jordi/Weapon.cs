using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : GunSubject
{
    public bool gunHeld = false;

    [SerializeField] protected GameObject Bullethole;
    [SerializeField] protected AudioClip GunShotAudio;
    [SerializeField] protected AudioClip GunHitAudio;
    [SerializeField] protected AudioClip EnemyHitAudio;
    [SerializeField] protected GameObject hitAudioObject;

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
            if (Physics.Raycast(Bullethole.transform.position, Bullethole.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
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
