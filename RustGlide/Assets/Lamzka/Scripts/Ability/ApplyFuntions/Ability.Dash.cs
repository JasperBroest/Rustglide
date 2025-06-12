using UnityEngine;

public partial class AbilityObject
{
    [Space(15)] [Header("Dash Movement")] [Range(0, 10)]
    public float CooldownTimeMultiplyer;

    [Range(0, 10)] public float DashSpeedMultiplyer;
    public bool DoesEffectDash => CooldownTimeMultiplyer > 0 || DashSpeedMultiplyer > 0;
}

public partial class AblilityAbstract //Ability_Dash
{
    void ApplyDashAbility()
    {
        float cooldownTimeMultiplyer =
            CalculateProcentage(AbilityManager.Instance.CooldownTime, SO.CooldownTimeMultiplyer);
        AbilityManager.Instance.CooldownTime += cooldownTimeMultiplyer;

        float dashSpeedMultiplyer = CalculateProcentage(AbilityManager.Instance.DashSpeed, SO.DashSpeedMultiplyer);
        AbilityManager.Instance.DashSpeed += dashSpeedMultiplyer;

        _affectedStats["CooldownTimeMultiplyer"] = cooldownTimeMultiplyer;

        _affectedStats["CooldownTimeMultiplyer"] = dashSpeedMultiplyer;
    }
}