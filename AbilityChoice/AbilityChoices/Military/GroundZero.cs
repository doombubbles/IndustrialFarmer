using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Simulation.Towers.Emissions;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using UnhollowerBaseLib;

namespace AbilityChoice.AbilityChoices.Military
{
    public class GroundZero : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.GroundZero;

        protected override string Description1 =>
            "Bomb damage increased significantly. Occasionally drops mini Ground Zeros.";

        protected override string Description2 =>
            "Bomb damage increased significantly. Shoots a continuous stream of bombs.";

        protected const int Factor = 7;

        public override void Apply1(TowerModel model)
        {
            var pineapple = model.GetAttackModel("Pineapple");
            var ability = AbilityModel(model);
            var abilityAttack = ability.GetDescendant<AttackModel>().Duplicate();
            var abilityWeapon = abilityAttack.weapons[0];
            var effectModel = ability.GetBehavior<CreateEffectOnAbilityModel>().effectModel.Duplicate();
            effectModel.scale = .5f;
            effectModel.useCenterPosition = false;
            var sound = ability.GetBehavior<CreateSoundOnAbilityModel>().sound;

            var newAttack = pineapple.Duplicate();
            newAttack.name = "AttackModel_GroundZero";

            var weapon = newAttack.weapons[0];
            weapon.emission = abilityWeapon.emission;

            weapon.Rate = ability.Cooldown / Factor;
            abilityWeapon.projectile.GetDamageModel().damage /= Factor;
            abilityWeapon.projectile.radius = 100;
            if (abilityWeapon.projectile.HasBehavior(out SlowModel slowModel))
            {
                slowModel.lifespan /= Factor;
            }

            weapon.ejectY = 0;
            weapon.AddBehavior(new EjectEffectModel("EjectEffectModel_", "", effectModel, -1, false, false,
                true, false, false));

            var projectile = weapon.projectile;

            projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile = abilityWeapon.projectile;
            projectile.RemoveBehavior<CreateEffectOnExhaustFractionModel>();
            projectile.GetBehavior<DisplayModel>().display = "";

            weapon.AddBehavior(new CreateSoundOnProjectileCreatedModel(
                "CreateSoundOnProjectileCreatedModel_", sound, sound,
                sound, sound, sound, ""));

            model.AddBehavior(newAttack);
        }

        public override void Apply2(TowerModel model)
        {
            var pineapple = model.GetAttackModel("Pineapple");
            var emissionOverTime = pineapple.GetDescendant<EmissionOverTimeModel>();
            var pineappleWeapon = pineapple.weapons[0];
            pineappleWeapon.Rate = emissionOverTime.count * emissionOverTime.timeBetween;
            pineappleWeapon.RemoveBehavior<CheckAirUnitOverTrackModel>();
        }
    }
}