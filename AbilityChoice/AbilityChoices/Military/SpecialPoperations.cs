using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using UnhollowerBaseLib;

namespace AbilityChoice.AbilityChoices.Military
{
    public class SpecialPoperations : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.SpecialPoperations;

        protected override string Description1 =>
            "Occasionally deploys a powerful special Monkey Marine with machine gun.";

        protected override string Description2 => "A Monkey Marine attacks from inside the Heli with a machine gun.";

        public override void Apply1(TowerModel model)
        {
            var ability = AbilityModel(model);
            ability.enabled = false;
            var behavior =
                new ActivateAbilityAfterIntervalModel("ActivateAbilityAfterIntervalModel", ability, ability.Cooldown);
            model.AddBehavior(behavior);
        }

        public override void Apply2(TowerModel model)
        {
            var ability = AbilityModel(model);
            
            model.behaviors = model.behaviors.RemoveItem(ability);

            var marine = ability.GetBehavior<FindDeploymentLocationModel>().towerModel;

            var weapon = marine.GetAttackModels()[0].weapons[0].Duplicate();

            var airBehavior = model.GetAttackModels()[0].weapons[0].GetBehavior<FireFromAirUnitModel>();
            weapon.behaviors = new Il2CppReferenceArray<WeaponBehaviorModel>(new WeaponBehaviorModel[] {airBehavior});

            weapon.ejectX = weapon.ejectY = weapon.ejectZ = 0;

            weapon.emission = model.GetWeapon().emission.Duplicate();
            weapon.emission.Cast<EmissionWithOffsetsModel>().throwMarkerOffsetModels =
                new Il2CppReferenceArray<ThrowMarkerOffsetModel>(new[]
                {
                    weapon.emission.Cast<EmissionWithOffsetsModel>().throwMarkerOffsetModels[0]
                });
            weapon.emission.Cast<EmissionWithOffsetsModel>().throwMarkerOffsetModels[0].ejectX = 0;
            weapon.emission.Cast<EmissionWithOffsetsModel>().projectileCount = 1;

            model.GetAttackModels()[0].AddWeapon(weapon);
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