using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Military
{
    public class SupportDrop : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.SupportChinook;

        protected override string Description1 =>
            "Occasionally drops lives and cash crates. Can pick up and redeploy most Monkey types.";

        protected override string Description2 =>
            "Downdraft attack is stronger and damages Moab class bloons. Can pick up and redeploy most Monkey types.";

        public override void Apply1(TowerModel model)
        {
            TechBotify(model);
        }

        public override void Apply2(TowerModel model)
        {
            var downDraft = model.GetAttackModel("Downdraft");
            var projectileModel = downDraft.GetDescendant<ProjectileModel>();
            var windModel = projectileModel.GetBehavior<WindModel>();
            windModel.distanceMin = windModel.distanceMax;
            projectileModel.AddBehavior(new DamageModel("DamageModel_", 1, 0, true, false, true,
                BloonProperties.None));
            projectileModel.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 1, 9,
                false, true));
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