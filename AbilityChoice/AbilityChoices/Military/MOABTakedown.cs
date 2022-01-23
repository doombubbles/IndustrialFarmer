using System.Linq;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Filters;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using UnhollowerBaseLib;

namespace AbilityChoice.AbilityChoices.Military
{
    public class MOABTakedown : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.MonkeyPirates;

        protected override string Description1 =>
            "Adds 2 new cannons to the ship and cannons attacks do more damage. Also gains a hook attack which can periodically take-down MOABs or DDTs.";

        protected override string Description2 =>
            "Adds 2 new cannons to the ship and cannons attacks do more damage, further increased against MOABs and Ceramics.";

        private const int MoabWorthMultiplier = 6;
        protected virtual bool FilterBFBs => true;

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var hookAttack = abilityModel.GetDescendant<AttackModel>().Duplicate();
            hookAttack.weapons[0].Rate = abilityModel.Cooldown / MoabWorthMultiplier;
            if (FilterBFBs)
            {
                var filter = hookAttack.GetDescendant<AttackFilterModel>();
                var bfbFilter = new FilterOutTagModel("FilterOutTagModel_Grapple", "Bfb", new Il2CppStringArray(0));
                filter.filters = filter.filters.AddTo(bfbFilter);
                filter.AddChildDependant(bfbFilter);
            }

            model.AddBehavior(hookAttack);
        }

        public override void Apply2(TowerModel model)
        {
            foreach (var projectileModel in model.GetDescendants<ProjectileModel>()
                         .ToList()
                         .Where(projectileModel => projectileModel.id == "Explosion"))
            {
                projectileModel.AddBehavior(new DamageModifierForTagModel("MoabDamage", "Moabs", 1.0f, 20, false,
                    false));
                projectileModel.AddBehavior(new DamageModifierForTagModel("MoabDamage", "Ceramic", 1.0f, 10, false,
                    false));
            }
        }
    }
}