using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


public abstract partial class AblilityAbstract : MonoBehaviour
{
    //testing variable, remove later
    [Header("Testing button lmao")] public bool yes;


    [Header("Scriptable Object")] public AbilityObject SO;

    private Dictionary<string, float> _affectedStats = new Dictionary<string, float>();


    //Update for testing purposes
    private void Update()
    {
        if (yes)
        {
            ApplyAbility();
            yes = false;
        }
    }

    //Check if ability has cooldown
    public void ApplyAbility()
    {
        StartCoroutine(EffectTimer());
    }

    public IEnumerator EffectTimer()
    {
        if (SO == null)
        {
            Debug.LogError("<color='#fff'>SO is null</color>");
            yield break;
        }

        if (!SO.HasEffectDuration)
        {
            Debug.Log("NoEffectDuration");
            ApplyStatBoost(false);
            yield break;
        }

        Debug.Log("applied ability");
        
        ApplyStatBoost(false);
        
        Debug.Log(SO.EffectDuration);
        
       
        yield return new WaitForSeconds(SO.EffectDuration);
        
        
        Debug.Log("removed ability");
        ApplyStatBoost(true);

        // if (SO.HasDownside &&
        //     !SO.DoesDownsideHaveDuration)
        // {
        //     yield return new WaitForSeconds(SO.EffectDuration);
        // }
    }

    private void ApplyStatBoost(bool Remove)
    {
        ApplyOrRemove(Remove, SO.HasStaminaEffect, ApplyStaminaEffect, RemoveSpeedBoost);

        ApplyOrRemove(Remove, SO.HasWeaponMultipliers, ApplyWeaponEffect, RemoveWeaponEffect);

        ApplyOrRemove(Remove, SO.DoesEffectStandard, ApplyStandardSpeed, RemoveWeaponEffect);

        // ApplyOrRemove(Remove, SO.DoesEffectDash, ApplyDashAbility, null); // Uncomment if needed

        ApplyOrRemove(Remove, SO.DoesEffectBoost, ApplyBoosterAbility, RemoveBoosterAbility);

        ApplyOrRemove(Remove, SO.DoesEffectMonkey, ApplyMonkeyAbility, RemoveMonkeyAbility);
    }


    private void ApplyOrRemove(bool remove, bool hasEffect, Action apply, Action removeAction)
    {
        if (!remove && hasEffect)
            apply();
        else if (remove)
            removeAction?.Invoke(); // handles potential null remove actions
    }

    private float CalculateProcentage(float CurrentAmount, float MultiplyAmount)
    {
        return (MultiplyAmount / 100f) * CurrentAmount;
    }
}