using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System;
using NUnit.Framework.Internal;

public enum Hand { Left, Right, None }

public class Weapon : GunSubject
{
    [Range(0, 1)][SerializeField] protected float spreadFactor;
    [SerializeField] protected GameObject Bullethole;
    [SerializeField] protected AudioClip GunShotAudio;
    [SerializeField] protected AudioClip GunHitAudio;
    [SerializeField] protected AudioClip EnemyHitAudio;
    [SerializeField] protected GameObject hitAudioObject;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected int Range;

    public ProTubeSettings gunHaptic;
    public bool gunHeld = false;
    public Hand gunHoldingHand = Hand.None;

    protected bool cooldownCoroutineRunning = false;
    [SerializeField]protected int HandsHeld = 0;
    protected float dmg = 10;
    protected AudioSource gunShotSource;
    protected AudioSource gunHitSource;
    protected ParticleSystem gunHitParticle;
    protected VisualEffect gunShotParticle;
    protected float cooldown = 0f;
    protected bool onCooldown = false;
    protected bool RTriggerPressed;
    protected bool LTriggerPressed;
    protected bool RGripPressed;
    protected bool LGripPressed;
    protected XRBaseInteractor currentInteractor;
    private XRGrabInteractable grab;
    private DashToDirection dashToDirection;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnSelectEntered);
        grab.selectExited.AddListener(OnSelectExited);
        dashToDirection = FindAnyObjectByType<DashToDirection>();
    }

    private void OnDestroy()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnSelectEntered);
            grab.selectExited.RemoveListener(OnSelectExited);
        }
    }

    public void RightTrigger(bool RState)
    {
        RTriggerPressed = RState;
    }

    public void LeftTrigger(bool LState)
    {
        LTriggerPressed = LState;
    }

    public void RightGrip(bool RGrip)
    {
        RGripPressed = RGrip;
    }

    public void LeftGrip(bool LGrip)
    {
        LGripPressed = LGrip;
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            gunHoldingHand = DetermineHand(interactor);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null && HandsHeld == 0)
        {
            gunHoldingHand = Hand.None;
            dashToDirection.leftBoosterActivated = true;
            dashToDirection.rightBoosterActivated = true;
        }
    }

    private Hand DetermineHand(XRBaseInteractor interactor)
    {
        if (HandsHeld == 1)
        {
            if (interactor.gameObject.layer == LayerMask.NameToLayer("LeftHand"))
            {
                gunHoldingHand = Hand.Left;
                dashToDirection.leftBoosterActivated = false;
            }
            else if (interactor.gameObject.layer == LayerMask.NameToLayer("RightHand"))
            {
                gunHoldingHand = Hand.Right;
                dashToDirection.rightBoosterActivated = false;
            }
        }
        return gunHoldingHand;
    }

    protected void Shoot()
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

        if (gameObject.TryGetComponent<Shotgun>(out Shotgun shotgun))
        {
            for (int i = 0; i < shotgun.numberOfProjectiles; i++)
            {
                Vector3 direction = Bullethole.transform.forward;
                direction += Bullethole.transform.up * UnityEngine.Random.Range(-spreadFactor, spreadFactor);
                direction += Bullethole.transform.right * UnityEngine.Random.Range(-spreadFactor, spreadFactor);

                //shoots a raycast out of the bullethole of the gun and spawns a particle and sound effect on the place you hit
                RaycastHit hit;
                if (Physics.Raycast(Bullethole.transform.position, direction.normalized, out hit, Range))
                {
                    raycastShoot(hit);
                }
            }
        }
        else
        {
            Vector3 direction = Bullethole.transform.forward;
            direction += Bullethole.transform.up * UnityEngine.Random.Range(-spreadFactor, spreadFactor);
            direction += Bullethole.transform.right * UnityEngine.Random.Range(-spreadFactor, spreadFactor);

            //shoots a raycast out of the bullethole of the gun and spawns a particle and sound effect on the place you hit
            RaycastHit hit;
            if (Physics.Raycast(Bullethole.transform.position, direction.normalized, out hit, Range))
            {
                raycastShoot(hit);
            }
        }
    }

    private void raycastShoot(RaycastHit hit)
    {
        GameObject SpawnedObject = Instantiate(hitAudioObject);
        SpawnedObject.transform.parent = null;
        SpawnedObject.transform.position = hit.point;
        gunHitSource = SpawnedObject.GetComponent<AudioSource>();
        gunHitParticle = SpawnedObject.GetComponentInChildren<ParticleSystem>();

        //if the thing you hit is an enemy do damage to it and play enemy hit sound
        if (hit.collider.CompareTag("Enemy"))
        {
            StateController stateController = hit.collider.GetComponent<StateController>();
            stateController.DamageTaken = dmg;
            stateController.ChangeState(stateController.hurtState);
            hit.collider.GetComponent<StateController>().ChangeState(stateController.hurtState);
            gunHitSource.clip = EnemyHitAudio;
        }
        // if not then play normal hit sound
        else
        {
            gunHitSource.clip = GunHitAudio;
        }
        gunHitParticle.Play();
        gunHitSource.PlayOneShot(gunHitSource.clip);

        //delete the spawned object with the audio and hit sound after a couple seconds
        StartCoroutine(DeleteSource(SpawnedObject));
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
        yield return new WaitForSeconds(2f);
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
