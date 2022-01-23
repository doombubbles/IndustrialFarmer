using BTD_Mod_Helper.Api.Enums;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class AntiBloon : TechTerror
    {
        public override string UpgradeId => UpgradeType.TheAntiBloon;
        protected override string AbilityName => ""; //weird

        protected override string Description1 => "Frequently eradicates nearby Bloons.";
        protected override string Description2 => "Nanobot plasma seeks out and destroys Bloons with strong Crits";
    }
}