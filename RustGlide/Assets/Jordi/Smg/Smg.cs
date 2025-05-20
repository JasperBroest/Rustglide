using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Smg : GunSubject
{
    public bool gunHeld = false;


    [SerializeField] private GameObject Bullethole;
    [SerializeField] private AudioClip GunShotAudio;
    [SerializeField] private AudioClip GunHitAudio;
    [SerializeField] private AudioClip EnemyHitAudio;
    [SerializeField] private GameObject hitAudioObject;
    [SerializeField] private GameObject holdster;
    [SerializeField] private GameObject gorillaHoldster;

    public InputAction Trigger;
    public ProTubeSettings gunHaptic;

    private int dmg = 10;
    private AudioSource gunShotSource;
    private AudioSource gunHitSource;
    private ParticleSystem gunHitparticle;
    private ParticleSystem GunShotParticle;
    private bool onCooldown = false;

    private void OnEnable()
    {
        Trigger.Enable();
    }



    private void Start()
    {
        gunShotSource = GetComponent<AudioSource>();
        GunShotParticle = GetComponentInChildren<ParticleSystem>();
        gunShotSource.clip = GunShotAudio;
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
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

    public void OnGrab()
    {
        gunHeld = true;
        GetComponent<Rigidbody>().isKinematic = false;
        NotifyIsGrabbed(gunHeld);
    }

    public void OnRelease()
    {
        gunHeld = false;
        GetComponent<Rigidbody>().isKinematic = true;
        NotifyIsGrabbed(gunHeld);
    }
}
