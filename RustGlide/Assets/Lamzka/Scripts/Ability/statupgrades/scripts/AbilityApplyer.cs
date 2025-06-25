using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class AbilityApplyer : AblilityAbstract, IPlayerInput,IAbilityHasBeenChosen
{
    public ParticleSystem EffectOnApply;

    public AudioSource AudioSource;
    public AudioClip SoundOnApply;
    public GameObject visual;

    private bool isUsed;
    
    private bool rTrigger;
    private bool lTrigger;
    private bool isGrabbed;

    private void Start()
    {
        isUsed = false;
        GetInput();
    }

    public bool HasBeenChosen(bool State)
    {
        throw new NotImplementedException();
    }

    public void RightTrigger(bool RState)
    {
        rTrigger = RState;
    }

    public void LeftTrigger(bool LState)
    {
        lTrigger = LState;
    }
    

    public void OnGrab(bool State)
    {
        isGrabbed = State;
    }

    public void Update()
    {
        if (!isUsed) CheckForActivation();
        
    }


    private void CheckForActivation()
    {
        if (isGrabbed & lTrigger ||isGrabbed & rTrigger)
        {
            isUsed = true;
            HasBeenChosen(true);
            ApplyAbility();
            StartCoroutine(WaitBeforeDestroy());
        }
    }

    private void OnApply()
    {
        Destroy(visual);
        AudioSource.PlayOneShot(SoundOnApply);
        EffectOnApply.Play();
    }
    
    
    private void GetInput()
    {
        GameObject CurrentInput = GameObject.FindWithTag("PlayerInput");
        CurrentInput.GetComponent<InputSubject>().AddObserver(this);
    }

    IEnumerator WaitBeforeDestroy()
    {
        OnApply();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
    
    
}
