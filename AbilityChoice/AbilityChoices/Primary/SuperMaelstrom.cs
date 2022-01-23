using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class SuperMaelstrom : BladeMaelstrom
    {
        public override string UpgradeId => UpgradeType.SuperMaelstrom;
        
        protected override string Description1 => "Shoots out a faster and larger swirl of global, high pierce blades.";
        protected override string Description2 => "Shoots out more homing blades with increased range and pierce.";

        protected override int Pierce => 9;
        protected override float Lifespan => 16;

        public override void Apply1(TowerModel model)
        {
            base.Apply1(model);

            if (Mode2)
            {
                model.GetDescendant<ArcEmissionModel>().count *= 2;
                model.range += 11;
                model.GetAttackModel().range += 11;
            }
        }
    }
}