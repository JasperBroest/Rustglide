﻿using UnityEngine;

public partial class AbilityObject
{
    [Space(15)] [Header("Weapon Multiplyers")] [Range(0, 300)]
    public float DamageMultiplyer;

    [Space(15)] [Range(0, 300)] public float WeaponSpeedMultiplyer;
    public bool HasWeaponMultipliers => DamageMultiplyer > 0 || WeaponSpeedMultiplyer > 0;
    
    
}


public partial class AblilityAbstract // Weapon
{
    private const string damageMultiplierKey = "DamageMultiplier";
    private const string speedMultiplierKey = "WeaponSpeedMultiplyer";

    private void ApplyWeaponEffect()
    {
        float damageMultiplier = CalculateProcentage(AbilityManager.Instance.CurrentSMGShootingCooldown, SO.DamageMultiplyer);
        AbilityManager.Instance.CurrentSMGShootingCooldown += damageMultiplier;

        float weaponSpeedMultiplier =
            CalculateProcentage(AbilityManager.Instance.CurrentSMGShootingCooldown, SO.WeaponSpeedMultiplyer);
        AbilityManager.Instance.CurrentSMGShootingCooldown += weaponSpeedMultiplier;


        _affectedStats["DamageMultiplyer"] = damageMultiplier;
        _affectedStats["WeaponSpeedMultiplyer"] = weaponSpeedMultiplier;
    }

    private void RemoveWeaponEffect()
    {
        if (_affectedStats.TryGetValue("DamageMultiplyer", out float damageMultiplier))
        {
            AbilityManager.Instance.CurrentSMGShootingCooldown -= damageMultiplier;
            _affectedStats.Remove("DamageMultiplyer");
        }

        if (_affectedStats.TryGetValue("WeaponSpeedMultiplyer", out float speedMultiplier))
        {
            AbilityManager.Instance.CurrentSMGShootingCooldown -= speedMultiplier;
            _affectedStats.Remove("WeaponSpeedMultiplyer");
        }
    }
}