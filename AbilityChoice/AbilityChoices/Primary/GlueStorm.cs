using Assets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class GlueStorm : GlueStrike
    {
        public override string UpgradeId => UpgradeType.GlueStorm;
        protected override string Description1 => "Glue weakens and slows Bloons further. Range is increased.";

        public override void Apply1(TowerModel model)
        {
            base.Apply1(model);
            model.range *= 2;
            model.GetAttackModel().range *= 2;
            model.GetWeapon().rate /= 2f;
        }
    }
}