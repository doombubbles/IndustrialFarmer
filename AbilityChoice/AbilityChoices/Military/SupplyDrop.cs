using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Military
{
    public class SupplyDrop : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.SupplyDrop;

        protected override string Description1 =>
            "Occasionally drops crates of cash. Regular attack also damages Lead Bloons and increases Shrapnel popping power.";

        protected override string Description2 =>
            "Bullets can bounce twice as much. Regular attack also damages Lead Bloons and increases Shrapnel popping power.";


        public override void Apply1(TowerModel model)
        {
            TechBotify(model);
        }

        public override void Apply2(TowerModel model)
        {
            model.GetWeapon().projectile.pierce *= 2;
        }

        public override void RemoveAbility(TowerModel model)
        {
            if (Mode2)
            {
                base.RemoveAbility(model);
            }
        }
    }
}