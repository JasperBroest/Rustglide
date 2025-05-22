using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AblilityAbstract : MonoBehaviour
{
    //testing variable, remove later
    [Header("Testing button lmao")]
    public bool yes;


    [Header("Scriptable Object")]
    public AbilityObject SO;

    private Dictionary<string, float> affectedStats = new Dictionary<string, float>();


    //Update for testing purposes
    private void Update()
    {
        if (yes) ApplyAbility();
    }

    //Check if ability has cooldown
    void ApplyAbility()
    {
        EffectTimer();

    }


    //IM GONNA FUCKING KILL MYSELF
    private IEnumerator EffectTimer()
    {
        if (SO.HasEffectDuration)
        {
            ApplyOrRemoveStatBoost(false);
            yield return new WaitForSeconds(SO.EffectDuration);
            ApplyOrRemoveStatBoost(true);

            if (SO.HasDownside)
            {
                if (!SO.DoesDownsideHaveDuration)
                {
                    //Apply downside
                    yield return new WaitForSeconds(SO.EffectDuration);
                    //clear effect
                }
                else
                {
                    //apply downside
                    yield break;
                }
            }
            yield break;

        }
        else
        {
            ApplyOrRemoveStatBoost(false);
            yield break;
        }
    }


    private void ApplyOrRemoveStatBoost(bool ShouldRemoveStats)
    {
        if (SO.HasStaminaEffect)
        {
            float affectedAmount;

            if (ShouldRemoveStats)
            {
                AbilityManager.Instance.Stamina -= affectedStats["Stamina"];
                affectedStats.Remove("Stamina"); // Clean up entry
            }

            affectedAmount = CalculateProcentage(AbilityManager.Instance.Stamina, SO.StaminaMultiplyer);
            AbilityManager.Instance.Stamina += affectedAmount;

            affectedStats["Stamina"] = affectedAmount;


        }


        if (SO.HasWeaponMultipliers)
        {
            if (SO.HasStaminaEffect)
            {
                float DamageMultiplyer;
                float WeaponSpeedMultiplyer;

                if (ShouldRemoveStats)
                {
                    AbilityManager.Instance.WeaponDamage -= affectedStats["DamageMultiplyer"];
                    AbilityManager.Instance.ShootingCooldown -= affectedStats["WeaponSpeedMultiplyer"];

                    affectedStats.Remove("DamageMultiplyer");
                    affectedStats.Remove("WeaponSpeedMultiplyer");
                }

                DamageMultiplyer = CalculateProcentage(AbilityManager.Instance.WeaponDamage, SO.DamageMultiplyer);
                AbilityManager.Instance.WeaponDamage += DamageMultiplyer;

                WeaponSpeedMultiplyer = CalculateProcentage(AbilityManager.Instance.ShootingCooldown, SO.WeaponSpeedMultiplyer);
                AbilityManager.Instance.WeaponDamage += WeaponSpeedMultiplyer;

                affectedStats["DamageMultiplyer"] = DamageMultiplyer;
                affectedStats["WeaponSpeedMultiplyer"] = WeaponSpeedMultiplyer;


            }
        }

        if (SO.DoesEffectStandard)
        {
            float affectedAmount;


            if (ShouldRemoveStats)
            {
                AbilityManager.Instance.StandardSpeed -= affectedStats["StandardSpeed"];
                affectedStats.Remove("StandardSpeed");
            }

            affectedAmount = CalculateProcentage(AbilityManager.Instance.StandardSpeed, SO.StandardSpeedMultiplyer);
            AbilityManager.Instance.StandardSpeed += affectedAmount;

            affectedAmount = affectedStats["StandardSpeed"];

            affectedStats["StandardSpeed"] = affectedAmount;


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

            CooldownTimeMultiplyer = CalculateProcentage(AbilityManager.Instance.CooldownTime, SO.CooldownTimeMultiplyer);
            AbilityManager.Instance.CooldownTime += CooldownTimeMultiplyer;

            DashSpeedMultiplyer = CalculateProcentage(AbilityManager.Instance.DashSpeed, SO.DashSpeedMultiplyer);
            AbilityManager.Instance.DashSpeed += DashSpeedMultiplyer;

            CooldownTimeMultiplyer = affectedStats["CooldownTimeMultiplyer"];

            DashSpeedMultiplyer = affectedStats["CooldownTimeMultiplyer"];


        }

        if (SO.DoesEffectBoost)
        {
            float affectedAmount;

            if (ShouldRemoveStats)
            {
                AbilityManager.Instance.BoosterSpeed -= affectedStats["affectedAmount"];

                affectedStats.Remove("affectedAmount");
            }

            affectedAmount = CalculateProcentage(AbilityManager.Instance.BoosterSpeed, SO.SetBoosterSpeedMultiplyer);
            AbilityManager.Instance.BoosterSpeed += affectedAmount;

            affectedAmount = affectedStats["affectedAmount"];


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

            JumpMultiplyerMultiplyer = CalculateProcentage(AbilityManager.Instance.MonkeyJump, SO.JumpMultiplyerMultiplyer);
            AbilityManager.Instance.MonkeyJump += JumpMultiplyerMultiplyer;

            MaxJumpSpeedMultiplyer = CalculateProcentage(AbilityManager.Instance.DashSpeed, SO.DashSpeedMultiplyer);
            AbilityManager.Instance.MonkeyMaxJumpSpeed += MaxJumpSpeedMultiplyer;


            JumpMultiplyerMultiplyer = affectedStats["JumpMultiplyerMultiplyer"];
            MaxJumpSpeedMultiplyer = affectedStats["MaxJumpSpeedMultiplyer"];

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
        return (MultiplyAmount / 100) * CurrentAmount;
    }



}
