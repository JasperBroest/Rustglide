
using UnityEngine;

public partial class AbilityObject
{
    
    [Space(15)] [Header("Booster Movement")] [Range(0, 300)]
    public float SetBoosterSpeedMultiplyer;
    public bool DoesEffectBoost => SetBoosterSpeedMultiplyer > 0;

}

public partial class AblilityAbstract //Ability_Boosters
{
    void ApplyBoosterAbility()
    {
        float affectedAmount =
            CalculateProcentage(AbilityManager.Instance.BoosterSpeed, SO.SetBoosterSpeedMultiplyer);
        AbilityManager.Instance.BoosterSpeed += affectedAmount;

        _affectedStats["BoosterSpeed"] = affectedAmount;
    }

    void RemoveBoosterAbility()
    {
        AbilityManager.Instance.BoosterSpeed -= _affectedStats["BoosterSpeed"];
        _affectedStats.Remove("BoosterSpeed"); // Clean up entry
    }
}