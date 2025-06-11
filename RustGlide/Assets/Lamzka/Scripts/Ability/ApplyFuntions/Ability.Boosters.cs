
using UnityEngine;

public partial class AbilityObject
{
    [Space(15)] [Header("Booster Movement")] [Range(0, 10)]
    public float SetBoosterSpeedMultiplyer;

}

public partial class AblilityAbstract //Ability_Boosters
{
    void ApplyBoosterAbility()
    {
        float affectedAmount =
            CalculateProcentage(AbilityManager.Instance.BoosterSpeed, SO.SetBoosterSpeedMultiplyer);
        AbilityManager.Instance.BoosterSpeed += affectedAmount;

        affectedStats["affectedAmount"] = affectedAmount;
    }
}