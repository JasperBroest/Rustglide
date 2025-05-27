using UnityEngine;


[CreateAssetMenu]
public class AbilityObject : ScriptableObject
{
    //what the heeeeeeeellll
    //IM GOING TO FUCKING KILL MYSELF
    //THIS SCRIPT FUCKING SUCKS BALLS

    /*public bool HasEffect2
    {
        get
        {
            return Mathf.Approximately(EffectDuration, 0);
        }
    }*/


    [Header("===Stamina===")]
    public float StaminaMultiplyer;
    public bool HasStaminaEffect => StaminaMultiplyer > 0;


    [Header("===Normal Item Settings===")]
    [Range(0, 70)] public float EffectDuration;
    public bool HasEffectDuration => EffectDuration > 0;

    [Space(15)]

    [Header("Weapon Multiplyers")]
    [Range(0, 10)] public float DamageMultiplyer;
    [Range(0, 10)] public float WeaponSpeedMultiplyer;
    public bool HasWeaponMultipliers => DamageMultiplyer > 0 || WeaponSpeedMultiplyer > 0;

    [Space(15)]

    [Header("Standard Movement")]
    [Range(0, 10)] public float StandardSpeedMultiplyer;
    public bool DoesEffectStandard => StandardSpeedMultiplyer > 0;

    [Space(15)]

    [Header("Dash Movement")]
    [Range(0, 10)] public float CooldownTimeMultiplyer;
    [Range(0, 10)] public float DashSpeedMultiplyer;
    public bool DoesEffectDash => CooldownTimeMultiplyer > 0 || DashSpeedMultiplyer > 0;

    [Space(15)]

    [Header("Booster Movement")]
    [Range(0, 10)] public float SetBoosterSpeedMultiplyer;
    public bool DoesEffectBoost => SetBoosterSpeedMultiplyer > 0;

    [Space(15)]

    [Header("Monkey Movement")]
    [Range(0, 10)] public float JumpMultiplyerMultiplyer;
    [Range(0, 10)] public float MaxJumpSpeedMultiplyer;
    public bool DoesEffectMonkey => JumpMultiplyerMultiplyer > 0 || JumpMultiplyerMultiplyer > 0;

    [Space(50)]

    [Header("===Downside Settings===")]
    public bool HasDownside;
    [Range(0, 70)] public float DownsideDuration;
    public bool DoesDownsideHaveDuration => DownsideDuration > 0;

    [Space(15)]

    [Header("Stamina")]
    public float DownSideStaminaMultiplyer;

    public bool DownsideHasStaminaEffect => DownSideStaminaMultiplyer > 0;

    [Space(15)]

    [Header("Downside DamageMultiplyers")]
    [Range(0, 10)] public float DownsideDamageMultiplyer;
    public bool DoesDamageHaveDownside => DownsideDamageMultiplyer > 0;

    [Space(15)]

    [Header("Downside StandardMovement")]
    [Range(0, 10)] public float DownsideStandardSpeedMultiplyer;
    public bool DoesStandardHaveDownside => DownsideStandardSpeedMultiplyer > 0;

    [Space(15)]

    [Header("Downside DashMovement")]
    [Range(0, 10)] public float DownsideCooldownTimeMultiplyer;
    [Range(0, 10)] public float DownsideDashSpeedMultiplyer;
    public bool DoesDashHaveDownside => DownsideCooldownTimeMultiplyer > 0 || DownsideDashSpeedMultiplyer > 0;

    [Space(15)]

    [Header("Downside BoosterMovement")]
    [Range(0, 10)] public float DownsideSetBoosterSpeedMultiplyer;
    public bool DoesBoosterHaveDownside => DownsideSetBoosterSpeedMultiplyer > 0;

    [Space(15)]

    [Header("Downside MonkeyMovement")]
    [Range(0, 10)] public float DownsideJumpMultiplyerMultiplyer;
    [Range(0, 10)] public float DownsideMaxJumpSpeedMultiplyer;
    public bool DoesMonkeyMovementHaveDownside => DownsideJumpMultiplyerMultiplyer > 0 || DownsideMaxJumpSpeedMultiplyer > 0;
}
