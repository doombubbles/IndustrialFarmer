using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class PermaCharge : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.PermaCharge;
        protected override string Description1 => "Perma Charge has permanent super fast attack speed, and a moderate damage increase.";

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var damageUp = abilityModel.GetBehavior<DamageUpModel>();
            var damageModel = model.GetWeapon().projectile.GetDamageModel();

            var bonus = CalcAvgBonus(damageUp.lifespanFrames / (float) abilityModel.cooldownFrames,
                (damageUp.additionalDamage + damageModel.damage) / damageModel.damage);

            damageModel.damage += bonus;
        }
    }
}