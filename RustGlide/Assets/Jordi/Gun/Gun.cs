using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public InputAction Trigger;

    [SerializeField] private GameObject Bullethole;

    private int dmg = 10;

    [SerializeField] private ParticleSystem GunShotParticle;
    private AudioSource GunShotSource;
    [SerializeField] private AudioClip GunShotAudio;

    public ProTubeSettings gunHaptic;
    private AudioSource GunHitSource;
    private ParticleSystem GunHitparticle;
    [SerializeField] private AudioClip GunHitAudio;
    [SerializeField] private AudioClip EnemyHitAudio;
    [SerializeField] private GameObject hitAudioObject;

    private bool GunHeld = false;
    private bool OnCooldown = false;

    private void OnEnable()
    {
        Trigger.Enable();
    }

    private void Start()
    {
        GunShotSource = GetComponent<AudioSource>();
        GunShotSource.clip = GunShotAudio;
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Trigger.ReadValue<float>() > 0.1f && GunHeld && !OnCooldown)
        {
            GunShotParticle.Play();
            GunShotSource.PlayOneShot(GunShotSource.clip);
            ForceTubeVRInterface.Shoot(gunHaptic);
            RaycastHit hit;
            if (Physics.Raycast(Bullethole.transform.position, Bullethole.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                GameObject SpawnedObject = Instantiate(hitAudioObject);
                SpawnedObject.transform.parent = null;
                SpawnedObject.transform.position = hit.point;
                GunHitSource = SpawnedObject.GetComponent<AudioSource>();
                GunHitparticle = SpawnedObject.GetComponentInChildren<ParticleSystem>();
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(dmg);
                    GunHitSource.clip = EnemyHitAudio;
                }
                else
                {
                    GunHitSource.clip = GunHitAudio;
                }
                GunHitparticle.Play();
                GunHitSource.PlayOneShot(GunHitSource.clip);
                StartCoroutine(DeleteSource(SpawnedObject));
            }
            OnCooldown = true;
        }
        if (Trigger.ReadValue<float>() == 0 && GunHeld)
        {
            OnCooldown = false;
        }
        Debug.Log(OnCooldown);
    }

    public IEnumerator SetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        OnCooldown = false;

    }

    public IEnumerator DeleteSource(GameObject gameObject)
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject.gameObject);
    }

    public void OnGrab()
    {
        GunHeld = true;
    }

    public void OnRelease()
    {
        GunHeld = false;
    }
}
