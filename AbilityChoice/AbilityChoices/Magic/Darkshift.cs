using Assets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class Darkshift : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.DarkKnight;

        protected override string Description1 =>
            "Dark blades increase knockback and pierce and deal extra damage to MOAB-class Bloons. Increased range.";
        
        public override void Apply1(TowerModel model)
        {
            model.IncreaseRange(10);
        }
    }
}