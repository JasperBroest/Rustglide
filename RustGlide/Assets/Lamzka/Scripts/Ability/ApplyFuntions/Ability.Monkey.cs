public partial class AblilityAbstract //Ability_Monkey
{
    void ApplyMonkeyAbility()
    {
        float JumpMultiplyerMultiplyer =
            CalculateProcentage(AbilityManager.Instance.MonkeyJump, SO.JumpMultiplyerMultiplyer);
        AbilityManager.Instance.MonkeyJump += JumpMultiplyerMultiplyer;

        float MaxJumpSpeedMultiplyer = CalculateProcentage(AbilityManager.Instance.DashSpeed, SO.DashSpeedMultiplyer);
        AbilityManager.Instance.MonkeyMaxJumpSpeed += MaxJumpSpeedMultiplyer;


        affectedStats["JumpMultiplyerMultiplyer"] = JumpMultiplyerMultiplyer;
        affectedStats["MaxJumpSpeedMultiplyer"] = MaxJumpSpeedMultiplyer;
    }
}