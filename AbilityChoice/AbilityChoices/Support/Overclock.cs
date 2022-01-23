using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Support
{
    public class Overclock : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.Overclock;

        protected override string Description1 =>
            "Modified Ability: Permanently boost (based on tier) one tower at a time.";

        protected override string Description2 => "All towers in range have increased Attack Speed.";

        protected virtual float Multiplier => .8f;
        
        public override void Apply1(TowerModel model)
        {
            // see OverclockHandler
        }

        public override void Apply2(TowerModel model)
        {
            var overclock = AbilityModel(model).GetBehavior<OverclockModel>();

            var rateSupport = new RateSupportModel("RateSupportModel_", Multiplier, true,
                overclock.mutatorSaveId, false, 1, null, overclock.buffLocsName, overclock.buffIconName)
            {
                appliesToOwningTower = true,
                showBuffIcon = true
            };
            
            model.AddBehavior(rateSupport);
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