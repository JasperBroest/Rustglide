using UnityEngine;

public partial class AbilityObject // Stamina
{
    [Space(15)] [Header("===Stamina===")] public float StaminaMultiplyer;
    public bool HasStaminaEffect => StaminaMultiplyer > 0;
}

public partial class AblilityAbstract // Stamina
{
    private void ApplyStaminaEffect()
    {
        float affectedAmount = CalculateProcentage(AbilityManager.Instance.Stamina, SO.StaminaMultiplyer);
        AbilityManager.Instance.Stamina += affectedAmount;
        _affectedStats["Stamina"] = affectedAmount;
        
    }

    private void RemoveStaminaEffect()
    {
        AbilityManager.Instance.Stamina -= _affectedStats["Stamina"];
        _affectedStats.Remove("Stamina"); // Clean up entry
        
        
    }
    
    
}