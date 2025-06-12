using UnityEngine;

public partial class AbilityObject
{
    [Space(15)] [Header("Standard Movement")] [Range(0, 10)]
    public float StandardSpeedMultiplyer;

    public bool DoesEffectStandard => StandardSpeedMultiplyer > 0;
}

public partial class AblilityAbstract //Ability_StandardSpeed
{
    private void ApplyStandardSpeed()
    {
        float affectedAmount = CalculateProcentage(AbilityManager.Instance.StandardSpeed, SO.StandardSpeedMultiplyer);
        AbilityManager.Instance.StandardSpeed += affectedAmount;

        _affectedStats["StandardSpeed"] = affectedAmount;
        Debug.Log("Applyed sped");
    }

    private void  RemoveSpeedBoost()
    {
        AbilityManager.Instance.StandardSpeed -= _affectedStats["StandardSpeed"];
        _affectedStats.Remove("StandardSpeed");
        Debug.Log("removed sped");
    }
}