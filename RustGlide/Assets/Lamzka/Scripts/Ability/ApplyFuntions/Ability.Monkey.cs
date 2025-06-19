using UnityEngine;

public partial class AbilityObject
{
    [Space(15)] [Header("Monkey Movement")] [Range(0, 300)]
    public float JumpMultiplyerMultiplyer;

    [Range(0, 300)] public float MaxJumpSpeedMultiplyer;
    public bool DoesEffectMonkey => JumpMultiplyerMultiplyer > 0 || JumpMultiplyerMultiplyer > 0;
}


public partial class AblilityAbstract //Ability_Monkey
{
    void ApplyMonkeyAbility()
    {
        float JumpMultiplyerMultiplyer =
            CalculateProcentage(AbilityManager.Instance.MonkeyJump, SO.JumpMultiplyerMultiplyer);
        AbilityManager.Instance.MonkeyJump += JumpMultiplyerMultiplyer;

        float MaxJumpSpeedMultiplyer = CalculateProcentage(AbilityManager.Instance.DashSpeed, SO.DashSpeedMultiplyer);
        AbilityManager.Instance.MonkeyMaxJumpSpeed += MaxJumpSpeedMultiplyer;


        _affectedStats["JumpMultiplyerMultiplyer"] = JumpMultiplyerMultiplyer;
        _affectedStats["MaxJumpSpeedMultiplyer"] = MaxJumpSpeedMultiplyer;
    }

    public void RemoveMonkeyAbility()
    {
        AbilityManager.Instance.MonkeyJump -= _affectedStats["JumpMultiplyerMultiplyer"];
        AbilityManager.Instance.MonkeyMaxJumpSpeed -= _affectedStats["MaxJumpSpeedMultiplyer"];

        _affectedStats.Remove("JumpMultiplyerMultiplyer");
        _affectedStats.Remove("MaxJumpSpeedMultiplyer");
    }
}