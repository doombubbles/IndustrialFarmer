using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Support
{
    public class MonkeyNomics : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.MonkeyNomics;
        protected override string AbilityName => UpgradeId;
        
        protected override string Description1 => "For when you're too big to fail... such that you periodically get $10,000.";
        protected override string Description2 => "For when you're too big to fail... such that Bank Capacity becomes $27,500";
        
        public override void Apply1(TowerModel model)
        {
            TechBotify(model);
        }
        
        public override void Apply2(TowerModel model)
        {
            model.GetBehavior<BankModel>().capacity += 17500;
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