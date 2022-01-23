using BTD_Mod_Helper.Api.Enums;

namespace AbilityChoice.AbilityChoices.Support
{
    public class Ultraboost : Overclock
    {
        public override string UpgradeId => UpgradeType.Ultraboost;
        
        
        protected override string Description2 => "All towers in range have further increased Attack Speed.";
    }
}