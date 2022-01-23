using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class TransformingTonic : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.TransformingTonic;

        protected override string Description1 => "Gains a monstrous laser beam attack.";
        
        public override void Apply1(TowerModel model)
        {
            var ability = AbilityModel(model);
            var activateAttackModel = ability.GetBehavior<ActivateAttackModel>();
            var abilityAttack = activateAttackModel.attacks[0].Duplicate();
            var abilityWeapon = abilityAttack.weapons[0];
            var uptime = activateAttackModel.Lifespan / ability.Cooldown;

            abilityAttack.range = model.range;
            abilityWeapon.rate /= uptime;

            model.AddBehavior(abilityAttack);
            
            
            model.IncreaseRange(ability.GetBehavior<IncreaseRangeModel>().addative * uptime);
        }
    }
}