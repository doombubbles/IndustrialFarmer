using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Military
{
    public class Bombardment : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.ArtilleryBattery;

        protected override string Description1 => "Main attack upgrades to 3+ barrels for extremely fast attacks.";

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var turbo = abilityModel.GetBehavior<TurboModel>();

            var bonus = CalcAvgBonus(turbo.Lifespan / abilityModel.Cooldown, 1 / turbo.multiplier);
            model.GetWeapon().Rate /= bonus;
        }
    }
}