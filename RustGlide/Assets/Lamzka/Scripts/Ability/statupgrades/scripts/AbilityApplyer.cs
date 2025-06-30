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

    public bool isUsed;
    
    private bool rTrigger;
    private bool lTrigger;
    public bool isGrabbed;

    private void Start()
    {
        isUsed = false;
        GetInput();
    }

    public bool HasBeenChosen(bool State)
    {
        if (State) DestroyYourselfNOW();
        
        Debug.Log(State);
        Debug.Log(isUsed);
        return State;
    }

    public void RightTrigger(bool RState)
    {
        rTrigger = RState;
    }

    public void LeftTrigger(bool LState)
    {
        lTrigger = LState;
    }

    public void RightGrip(bool RGrip)
    {

    }

    public void LeftGrip(bool RGrip)
    {

    }


    public void OnGrab(bool State)
    {
        try
        {
            GameObject.FindGameObjectWithTag("RogueLikeManager").GetComponent<RogueLikeManager>().OnGrab();
        }
        catch (Exception e)
        {
           Debug.Log("Stop cheating!!!");
            throw;
        }
        
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

    private void DestroyYourselfNOW()
    {
        if (!isUsed)
        {
            Destroy(gameObject);
        }
    }
    
    IEnumerator WaitBeforeDestroy()
    {
        OnApply();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
    
    
}
