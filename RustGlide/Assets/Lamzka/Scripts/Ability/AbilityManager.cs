using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("this Instance")]
    public static AbilityManager Instance;

    [Header("===Weapon Stats===")]
    public float WeaponDamage;
    public float ShootingCooldown;

    [Space(20)]


    [Header("===PlayerStats===")]
    public float Stamina;

    [Header("Standard Movement")]
    public float StandardSpeed;

    [Header("Dash Movement")]
    public float DashSpeed;
    public float CooldownTime;

    [Header("Booster Movement")]
    public float BoosterSpeed;
    public float BoosterCooldown;

    [Header("Monkey Movement")]
    public float MonkeyJump;
    public float MonkeyMaxJumpSpeed;

    public bool TestBool;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }


    }
}
