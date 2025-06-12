using UnityEngine;


[CreateAssetMenu]
public partial class AbilityObject : ScriptableObject
{
    [Header("===Normal Item Settings===")] [Range(0, 70)]
    public float EffectDuration;

    public bool HasEffectDuration => EffectDuration > 0;


    [Space(50)] [Header("===Downside Settings===")]
    public bool HasDownside;

    [Range(0, 70)] public float DownsideDuration;
    public bool DoesDownsideHaveDuration => DownsideDuration > 0;

    [Space(15)] [Header("Stamina")] public float DownSideStaminaMultiplyer;

    public bool DownsideHasStaminaEffect => DownSideStaminaMultiplyer > 0;

    [Space(15)] [Header("Downside DamageMultiplyers")] [Range(0, 10)]
    public float DownsideDamageMultiplyer;

    public bool DoesDamageHaveDownside => DownsideDamageMultiplyer > 0;

    [Space(15)] [Header("Downside StandardMovement")] [Range(0, 10)]
    public float DownsideStandardSpeedMultiplyer;

    public bool DoesStandardHaveDownside => DownsideStandardSpeedMultiplyer > 0;

    [Space(15)] [Header("Downside DashMovement")] [Range(0, 10)]
    public float DownsideCooldownTimeMultiplyer;

    [Range(0, 10)] public float DownsideDashSpeedMultiplyer;
    public bool DoesDashHaveDownside => DownsideCooldownTimeMultiplyer > 0 || DownsideDashSpeedMultiplyer > 0;

    [Space(15)] [Header("Downside BoosterMovement")] [Range(0, 10)]
    public float DownsideSetBoosterSpeedMultiplyer;

    public bool DoesBoosterHaveDownside => DownsideSetBoosterSpeedMultiplyer > 0;

    [Space(15)] [Header("Downside MonkeyMovement")] [Range(0, 10)]
    public float DownsideJumpMultiplyerMultiplyer;

    [Range(0, 10)] public float DownsideMaxJumpSpeedMultiplyer;

    public bool DoesMonkeyMovementHaveDownside =>
        DownsideJumpMultiplyerMultiplyer > 0 || DownsideMaxJumpSpeedMultiplyer > 0;
}