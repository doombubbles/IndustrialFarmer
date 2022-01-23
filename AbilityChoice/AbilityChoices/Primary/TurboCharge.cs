using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class TurboCharge : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.TurboCharge;
        protected override string Description1 => "Further increased attack speed.";

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var turbo = abilityModel.GetBehavior<TurboModel>();
            var damage = model.GetWeapon().projectile.GetDamageModel().damage;

            var bonus = CalcAvgBonus(turbo.Lifespan / abilityModel.Cooldown,
                (damage + turbo.extraDamage) / damage / turbo.multiplier);
            model.GetWeapon().Rate /= bonus;
        }
    }
}