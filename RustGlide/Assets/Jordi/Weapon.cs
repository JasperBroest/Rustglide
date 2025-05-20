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
    [SerializeField] protected GameObject holdster;

    public InputAction Trigger;
    public ProTubeSettings gunHaptic;

    protected int dmg = 10;
    protected AudioSource gunShotSource;
    protected AudioSource gunHitSource;
    protected ParticleSystem gunHitparticle;
    protected ParticleSystem GunShotParticle;
    protected bool onCooldown = false;

    protected void Shoot()
    {
        if (Trigger.ReadValue<float>() > 0.1f && gunHeld && !onCooldown)
        {
            GunShotParticle.Play();
            gunShotSource.PlayOneShot(gunShotSource.clip);
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
            onCooldown = true;
        }
        if (Trigger.ReadValue<float>() == 0 && gunHeld)
        {
            onCooldown = false;
        }
    }

    public IEnumerator SetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        onCooldown = false;
    }

    public IEnumerator DeleteSource(GameObject gameObject)
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject.gameObject);
    }
}
