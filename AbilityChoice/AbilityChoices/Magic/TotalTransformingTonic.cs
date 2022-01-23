using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class TotalTransformingTonic : TransformingTonic
    {
        public override string UpgradeId => UpgradeType.TotalTransformation;

        protected override string Description1 =>
            "Has a monstrous laser attack, and 2 nearby Monkeys are constantly transformed into monsters.";

        protected override string Description2 =>
            "Transforms just itself permanently into an even more powerful monster!";

        public override void Apply1(TowerModel model)
        {
            base.Apply1(model);

            var abilityModel = AbilityModel(model);
            abilityModel.enabled = false;
            var morphTowerModel = abilityModel.GetBehavior<MorphTowerModel>();

            morphTowerModel.maxTowers = 2;

            const int interval = 5;
            abilityModel.Cooldown = interval;

            morphTowerModel.lifespanFrames = interval * 60 - 1;
            morphTowerModel.lifespan= morphTowerModel.lifespanFrames / 60f;

            morphTowerModel.effectModel.assetId = "";
            morphTowerModel.effectOnTransitionBackModel.assetId = "";

            abilityModel.RemoveBehavior<CreateEffectOnAbilityModel>();
            abilityModel.RemoveBehavior<CreateSoundOnAbilityModel>();
            abilityModel.RemoveBehavior<IncreaseRangeModel>();
            abilityModel.RemoveBehavior<ActivateAttackModel>();
            abilityModel.RemoveBehavior<SwitchDisplayModel>();
            
            model.AddBehavior(new ActivateAbilityAfterIntervalModel("ActivateAbilityAfterIntervalModel_", abilityModel,
                interval));
        }

        public override void Apply2(TowerModel model)
        {
            var ability = AbilityModel(model);
            var abilityAttack = ability.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            var abilityWeapon = abilityAttack.weapons[0];
            abilityWeapon.Rate /= 2;
            abilityWeapon.projectile.pierce *= 3;
            abilityWeapon.projectile.GetDamageModel().damage *= 2;
            
            abilityAttack.range = model.range;

            model.AddBehavior(abilityAttack);

            model.IncreaseRange(ability.GetBehavior<IncreaseRangeModel>().addative);
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