using Assets.Scripts.Models.Effects;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppSystem.Collections.Generic;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class Sabotage : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.BloonSabotage;

        protected override string Description1 => "All Bloons move at partially reduced speed.";
        protected override string Description2 => "Ninja’s attacks have more range and slow Bloons to half speed.";

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var slow = abilityModel.GetDescendant<SlowMinusAbilityDurationModel>();
            
            var mult = CalcAvgBonus(slow.Lifespan / abilityModel.Cooldown, slow.multiplier);
            
            
        }

        public override void Apply2(TowerModel model)
        {
            model.IncreaseRange(10);
            var ability = AbilityModel(model);
            var abilityAttack = ability.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            var abilityWeapon = abilityAttack.weapons[0];
            var slowMutator = abilityWeapon.projectile.GetBehavior<SlowMinusAbilityDurationModel>().Mutator;

            var dontSlowBadBehavior = abilityWeapon.projectile.GetBehavior<SlowModifierForTagModel>();

            var slowBehavior = new SlowModel("Sabotage", 0f, 2f, slowMutator.mutationId, "", 999,
                new Dictionary<string, AssetPathModel>(), 0, true, false, null,
                false, false) {mutator = slowMutator};


            foreach (var weaponModel in model.GetWeapons())
            {
                if (weaponModel.projectile.GetDamageModel().IsType(out DamageModel damageModel))
                {
                    weaponModel.projectile.AddBehavior(slowBehavior);
                    weaponModel.projectile.AddBehavior(dontSlowBadBehavior);
                    weaponModel.projectile.pierce += 5;

                    damageModel.immuneBloonProperties = BloonProperties.None;
                    
                }
            }
        }
    }
}