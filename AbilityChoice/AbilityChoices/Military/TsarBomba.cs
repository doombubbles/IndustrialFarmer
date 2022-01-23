using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Military
{
    public class TsarBomba : GroundZero
    {
        public override string UpgradeId => UpgradeType.TsarBomba;

        protected override string Description1 => "Occasionally drops mini Tsar Bombs.";
        protected override string Description2 => "Drops a continuous stream of larger, more powerful bombs that stun bloons.";

        public override void Apply2(TowerModel model)
        {
            base.Apply2(model);
            
            var slow = AbilityModel(model).GetDescendant<SlowModel>().Duplicate();
            var attackModel = model.GetAttackModel("Pineapple");
            slow.Lifespan /= Factor;

            var projectile = attackModel.GetDescendant<CreateProjectileOnExhaustFractionModel>().projectile;
                
            projectile.AddBehavior(slow);
            projectile.GetDamageModel().damage *= Factor;

            projectile.radius *= 2;
            attackModel.GetDescendant<CreateEffectOnExhaustFractionModel>().effectModel.scale /= 2f;
        }
    }
}