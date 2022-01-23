using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Support
{
    public class IMFLoan : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.IMFLoan;
        protected override string AbilityName => UpgradeId;

        protected override string Description1 => "Periodically gives you $10,000 that has to be paid back over time.";
        protected override string Description2 => "Bank capacity is increased to a base of $17,500";

        public override void Apply1(TowerModel model)
        {
            TechBotify(model);
        }

        public override void Apply2(TowerModel model)
        {
            model.GetBehavior<BankModel>().capacity += 7500;
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