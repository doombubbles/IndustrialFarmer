using BTD_Mod_Helper.Api.Enums;

namespace AbilityChoice.AbilityChoices.Military
{
    public class RocketStormMAD : RocketStorm
    {
        public override string UpgradeId => UpgradeType.MAD;
        
        protected override string AbilityName => Name;

        protected override string Description1 => "Occasionally shoots a wave of MAD Rocket Storm missiles.";
        protected override string Description2 => "Shoots a single stream of MAD Rocket Storm missiles with the same accuracy of its main attack.";
    }
}