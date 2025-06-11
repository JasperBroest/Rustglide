using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : GunSubject
{
    [SerializeField] protected GameObject Bullethole;
    [SerializeField] protected AudioClip GunShotAudio;
    [SerializeField] protected AudioClip GunHitAudio;
    [SerializeField] protected AudioClip EnemyHitAudio;
    [SerializeField] protected GameObject hitAudioObject;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected int Range;

    public InputAction Trigger;
    public ProTubeSettings gunHaptic;
    public bool gunHeld = false;

    protected bool cooldownCoroutineRunning = false;
    protected int HandsHeld = 0;
    protected float dmg = 10;
    protected AudioSource gunShotSource;
    protected AudioSource gunHitSource;
    protected ParticleSystem gunHitparticle;
    protected ParticleSystem GunShotParticle;
    protected float cooldown = 0f;
    protected bool onCooldown = false;

    private void FixedUpdate()
    {
        //dmg = AbilityManager.Instance.WeaponDamage;
        //cooldown = AbilityManager.Instance.ShootingCooldown;
    }

    protected void Shoot()
    {
        //if you click the trigger, hold the gun and it isn't on cooldown plays particle, sound and haptic feedback
        if (Trigger.ReadValue<float>() > 0.1 && gunHeld && !onCooldown)
        {
            onCooldown = true;
            if (Animator != null)
            {
                Animator.SetBool("Shooting", true);
            }
            GunShotParticle.Play();
            gunShotSource.PlayOneShot(GunShotAudio);
            ForceTubeVRInterface.Shoot(gunHaptic);

            //shoots a raycast out of the bullethole of the gun and spawns a particle and sound effect on the place you hit
            RaycastHit hit;
            if (Physics.Raycast(Bullethole.transform.position, Bullethole.transform.TransformDirection(Vector3.forward), out hit, Range))
            {
                GameObject SpawnedObject = Instantiate(hitAudioObject);
                SpawnedObject.transform.parent = null;
                SpawnedObject.transform.position = hit.point;
                gunHitSource = SpawnedObject.GetComponent<AudioSource>();
                gunHitparticle = SpawnedObject.GetComponentInChildren<ParticleSystem>();

                //if the thing you hit is an enemy do damage to it and play enemy hit sound
                if (hit.collider.CompareTag("Enemy"))
                {
                    //StateController stateController = hit.collider.GetComponent<StateController>();
                    //stateController.ChangeState(stateController.HurtState);
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(dmg);
                    gunHitSource.clip = EnemyHitAudio;
                }
                // if not then play normal hit sound
                else
                {
                    gunHitSource.clip = GunHitAudio;
                }
                gunHitparticle.Play();
                gunHitSource.PlayOneShot(gunHitSource.clip);

                //delete the spawned object with the audio and hit sound after a couple seconds
                StartCoroutine(DeleteSource(SpawnedObject));
            }
        }
    }

    //sets the cooldown of the gun so it doesnt shoot every frame
    public IEnumerator SetCooldown()
    {
        cooldownCoroutineRunning = true;
        yield return new WaitForSeconds(cooldown / 2);
        Animator.SetBool("Shooting", false);
        yield return new WaitForSeconds(cooldown / 2);
        onCooldown = false;
        cooldownCoroutineRunning = false;
    }

    //delete the spawned object with the audio and hit sound after a couple seconds
    public IEnumerator DeleteSource(GameObject gameObject)
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject.gameObject);
    }

    //checks if you grab if you hold it and also check for two handed objects
    public void OnGrab()
    {
        HandsHeld++;
        if (HandsHeld > 0)
        {
            gunHeld = true;
            GetComponent<Rigidbody>().isKinematic = false;
            NotifyIsGrabbed(gunHeld);
            if(GameObject.FindGameObjectWithTag("ChooseWeapons") != null)
            {
                GameObject.FindGameObjectWithTag("ChooseWeapons").GetComponent<RogueLikeManager>().OnGrab();
            }    
        }
    }

    //checks if you release it if you hold it and also check for two handed objects
    public void OnRelease()
    {
        HandsHeld--;
        if (HandsHeld == 0)
        {
            GameObject.FindGameObjectWithTag("GunHolster").GetComponent<GunHolster>().GetGun();

            gunHeld = false;
            GetComponent<Rigidbody>().isKinematic = true;
            NotifyIsGrabbed(gunHeld);
        }
    }
}
