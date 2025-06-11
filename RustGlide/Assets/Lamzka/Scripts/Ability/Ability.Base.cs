using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


public abstract partial class AblilityAbstract : MonoBehaviour
{
    //testing variable, remove later
    [Header("Testing button lmao")] public bool yes;


    [Header("Scriptable Object")] public AbilityObject SO;

    private Dictionary<string, float> affectedStats = new Dictionary<string, float>();


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
            ApplyOrRemoveStatBoost(false);
            yield break;
        }

        ApplyOrRemoveStatBoost(false);
        yield return new WaitForSeconds(SO.EffectDuration);
        ApplyOrRemoveStatBoost(true);

        if (SO.HasDownside &&
            !SO.DoesDownsideHaveDuration)
        {
            yield return new WaitForSeconds(SO.EffectDuration);
        }
    }

    private void ApplyStatBoost()
    {
        if (SO.HasStaminaEffect)
            ApplyStaminaEffect();

        if (SO.HasWeaponMultipliers)
            ApplyWeaponEffect();

        if (SO.DoesEffectStandard)
            ApplyStandardSpeed();

        if (SO.DoesEffectDash)
            ApplyDashAbility();

        if (SO.DoesEffectBoost)
            ApplyBoosterAbility();

        if (SO.DoesEffectMonkey)
            ApplyMonkeyAbility();
    }

    private void ApplyOrRemoveStatBoost(bool ShouldRemoveStats)
    {
        if (SO.HasStaminaEffect)
        {
            float affectedAmount;

            if (ShouldRemoveStats)
            {
            }
            else
            {
            }
        }


        if (SO.HasWeaponMultipliers)
        {
            float DamageMultiplyer;
            float WeaponSpeedMultiplyer;

            if (ShouldRemoveStats)
            {
            }
            else
            {
            }
        }

        if (SO.DoesEffectStandard)
        {
            float affectedAmount;


            if (ShouldRemoveStats)
            {
            }
            else
            {
            }
        }

        if (SO.DoesEffectDash)
        {
            float CooldownTimeMultiplyer;
            float DashSpeedMultiplyer;

            if (ShouldRemoveStats)
            {
                AbilityManager.Instance.StandardSpeed -= affectedStats["CooldownTimeMultiplyer"];
                AbilityManager.Instance.StandardSpeed -= affectedStats["CooldownTimeMultiplyer"];

                affectedStats.Remove("CooldownTimeMultiplyer");
                affectedStats.Remove("DashSpeedMultiplyer");
            }
            else
            {
            }
        }

        if (SO.DoesEffectBoost)
        {
            float affectedAmount;

            if (ShouldRemoveStats)
            {
                AbilityManager.Instance.BoosterSpeed -= affectedStats["affectedAmount"];

                affectedStats.Remove("affectedAmount");
            }
            else
            {
            }
        }

        if (SO.DoesEffectMonkey)
        {
            float JumpMultiplyerMultiplyer;
            float MaxJumpSpeedMultiplyer;

            if (ShouldRemoveStats)
            {
                AbilityManager.Instance.MonkeyJump -= affectedStats["JumpMultiplyerMultiplyer"];
                AbilityManager.Instance.MonkeyMaxJumpSpeed -= affectedStats["MaxJumpSpeedMultiplyer"];

                affectedStats.Remove("JumpMultiplyerMultiplyer");
                affectedStats.Remove("MaxJumpSpeedMultiplyer");
            }
            else
            {
            }
        }
    }

    private void ApplyOrRemoveDownsides()
    {
        if (SO.DownsideHasStaminaEffect)
        {
            float affectedAmount;
        }

        if (SO.DoesDamageHaveDownside)
        {
            float affectedAmount;
        }

        if (SO.DoesStandardHaveDownside)
        {
            float affectedAmount;
        }

        if (SO.DoesDashHaveDownside)
        {
            float affectedAmount;
        }

        if (SO.DoesBoosterHaveDownside)
        {
            float affectedAmount;
        }

        if (SO.DoesMonkeyMovementHaveDownside)
        {
            float affectedAmount;
        }
    }

    private float CalculateProcentage(float CurrentAmount, float MultiplyAmount)
    {
        return (MultiplyAmount / 100f) * CurrentAmount;
    }
}