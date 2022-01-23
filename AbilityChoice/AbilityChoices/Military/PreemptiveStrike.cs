using BTD_Mod_Helper.Api.Enums;

namespace AbilityChoice.AbilityChoices.Military
{
    public class PreemptiveStrike : FirstStrikeCapability
    {
        public override string UpgradeId => UpgradeType.PreEmptiveStrike;
        protected override string AbilityName => "First Strike Capability";
        
        
        protected override string Description1 => $"{DefaultDescription} ({base.Description1})";
        protected override string Description2 => $"{DefaultDescription} ({base.Description2})";
    }
}