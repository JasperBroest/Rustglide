using UnityEngine;

public class Smg : Weapon
{
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
