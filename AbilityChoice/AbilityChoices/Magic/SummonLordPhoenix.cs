using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class SummonLordPhoenix : SummonPheonix
    {
        public override string UpgradeId => UpgradeType.WizardLordPhoenix;

        protected override string Description1 => "Summons a somewhat powerful Lava Phoenix.";
        
        protected override string Description2 => "Wizard gains the attacks of both Phoenixes itself (non-globally).";

        public override void Apply1(TowerModel model)
        {
            var permaBehavior = model.GetBehavior<TowerCreateTowerModel>().Duplicate();
            var abilityModel = AbilityModel(model);
            var uptime = abilityModel.GetDescendant<TowerModel>().GetBehavior<TowerExpireModel>().Lifespan / abilityModel.Cooldown;
            var lordPhoenix = abilityModel.GetDescendant<TowerModel>();

            lordPhoenix.behaviors = lordPhoenix.behaviors.RemoveItemOfType<Model, TowerExpireModel>();
            foreach (var weaponModel in lordPhoenix.GetWeapons())
            {
                weaponModel.rate /= uptime;
            }

            permaBehavior.towerModel = lordPhoenix;

            model.AddBehavior(permaBehavior);
        }

        public override void Apply2(TowerModel model)
        {
            base.Apply2(model);

            var towerCreateTowerModel = model.GetBehavior<TowerCreateTowerModel>();
            model.RemoveBehavior(towerCreateTowerModel);
            
            AddAttacksFromSubTower(model, towerCreateTowerModel.towerModel);
        }
    }
}