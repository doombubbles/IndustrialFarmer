using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class GlueStrike : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.GlueStrike;

        protected override string Description1 =>
            "Glue weakens Bloons, making them take increased damage and be vulnerable to Sharp sources.";

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var damageBoost = abilityModel.GetDescendant<AddBonusDamagePerHitToBloonModel>();
            var sharpWeak = abilityModel.GetDescendant<RemoveDamageTypeModifierModel>();
            var abilitySlow = abilityModel.GetDescendant<SlowModel>();

            foreach (var projectileModel in model.GetWeapon().GetDescendants<ProjectileModel>().ToList())
            {
                var slowModel = projectileModel.GetBehavior<SlowModel>();
                slowModel.lifespan = abilitySlow.lifespan;
                slowModel.layers = abilitySlow.layers;
                slowModel.mutator.multiplier = abilitySlow.Multiplier;

                if (damageBoost != null)
                {
                    projectileModel.AddBehavior(damageBoost.Duplicate());
                }

                if (sharpWeak != null)
                {
                    projectileModel.AddBehavior(sharpWeak.Duplicate());
                }
            }
        }
    }
}