using System.Collections.Generic;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class GrandSabotage : Sabotage
    {
        public override string UpgradeId => UpgradeType.GrandSaboteur;

        protected override string Description1 => "";

        protected override string Description2 =>
            "Ninja's attack have further increased range and pierce, and do more damage to stronger Bloon types.";

        public override void Apply1(TowerModel model)
        {
            base.Apply1(model);
        }

        public override void Apply2(TowerModel model)
        {
            base.Apply2(model);
            model.IncreaseRange(10);

            var tags = new List<string>
            {
                BloonTag.Moab,
                BloonTag.Bfb,
                BloonTag.Zomg,
                BloonTag.Ddt,
                BloonTag.Bad
            };

            foreach (var weaponModel in model.GetWeapons())
            {
                for (var i = 0; i < tags.Count; i++)
                {
                    if (weaponModel.projectile.GetDamageModel().IsType(out DamageModel damageModel))
                    {

                        var behavior = new DamageModifierForTagModel("DamageModifierForTagModel_" + i, tags[i], 1.0f,
                            10 * (i + 1), false, false) {tags = new[] {tags[i]}};
                        weaponModel.projectile.AddBehavior(behavior);
                        weaponModel.projectile.pierce += 10;

                        damageModel.immuneBloonProperties = BloonProperties.None;
                    }
                }
            }
        }
    }
}