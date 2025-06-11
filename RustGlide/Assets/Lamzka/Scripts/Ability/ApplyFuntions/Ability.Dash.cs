using UnityEngine;

public partial class AbilityObject
{
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

        affectedStats["CooldownTimeMultiplyer"] = cooldownTimeMultiplyer;

        affectedStats["CooldownTimeMultiplyer"] = dashSpeedMultiplyer;
        
        
    }
}