using UnityEngine;

public partial class AbilityObject
{
    [Space(15)] [Header("Weapon Multiplyers")] [Range(0, 10)]
    public float DamageMultiplyer;

    [Space(15)] [Range(0, 10)] public float WeaponSpeedMultiplyer;
    public bool HasWeaponMultipliers => DamageMultiplyer > 0 || WeaponSpeedMultiplyer > 0;
    
    
}


public partial class AblilityAbstract // Weapon
{
    private const string damageMultiplierKey = "DamageMultiplier";
    private const string speedMultiplierKey = "DamageMultiplier";

    private void ApplyWeaponEffect()
    {
        float damageMultiplier = CalculateProcentage(AbilityManager.Instance.WeaponDamage, SO.DamageMultiplyer);
        AbilityManager.Instance.WeaponDamage += damageMultiplier;

        float weaponSpeedMultiplier =
            CalculateProcentage(AbilityManager.Instance.ShootingCooldown, SO.WeaponSpeedMultiplyer);
        AbilityManager.Instance.WeaponDamage += weaponSpeedMultiplier;


        _affectedStats["DamageMultiplyer"] = damageMultiplier;
        _affectedStats["WeaponSpeedMultiplyer"] = weaponSpeedMultiplier;
    }

    private void RemoveWeaponEffect()
    {
        AbilityManager.Instance.WeaponDamage -= _affectedStats["DamageMultiplyer"];
        AbilityManager.Instance.ShootingCooldown -= _affectedStats["WeaponSpeedMultiplyer"];

        _affectedStats.Remove("DamageMultiplyer");
        _affectedStats.Remove("WeaponSpeedMultiplyer");
    }
}