using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Military
{
    public class EliteSupplyDrop : SupplyDrop
    {
        public override string UpgradeId => UpgradeType.EliteSniper;
        protected override string AbilityName => "Supply Drop";

        protected override string Description1 => "Occasionally drops more cash. Grants Elite targeting prio and faster reload to all snipers.";
        protected override string Description2 => "Bullets bounce even more, and can bounce back to the same target. Grants Elite targeting prio and faster reload to all snipers.";

        public override void Apply2(TowerModel model)
        {
            base.Apply2(model);
            var projectileModel = model.GetWeapon().projectile;
            projectileModel.pierce *= 2;
            var delay = projectileModel.GetBehavior<RetargetOnContactModel>().delay;
            projectileModel.AddBehavior(new ClearHitBloonsModel("ClearHitBloonsModel_", delay));
        }
    }
}