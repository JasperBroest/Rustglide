using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public InputAction Trigger;

    [SerializeField]private GameObject Bullethole;

    private int dmg = 10;

    [SerializeField] private ParticleSystem GunShotParticle;
    private AudioSource GunShotSource;
    [SerializeField] private AudioClip GunShotAudio;

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
        if(Trigger.ReadValue<float>() > 0.1f && GunHeld && !OnCooldown)
        {
            GunShotSource.Play();
            GunShotParticle.Play();
            RaycastHit hit;
            if (Physics.Raycast(Bullethole.transform.position, Bullethole.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(dmg);
                }
            }
            OnCooldown = true;
            StartCoroutine(SetCooldown());
        }
    }

    public IEnumerator SetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        OnCooldown = false;
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
