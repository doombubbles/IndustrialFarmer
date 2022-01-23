using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Military
{
    public class PopAndAwe : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.PopAndAwe;

        protected override string Description1 =>
            "Main attack gains additional bonus damage to stunned Bloons. Occasionally causes mini-Pop and Awe effects on target.";

        protected override string Description2 =>
            "Main attack gains additional bonus damage to stunned Bloons, and has turbo attack speed permanently";

        public override void Apply1(TowerModel model)
        {
            var realWeapon = model.GetWeapon();
            var ability = AbilityModel(model);
            var abilityAttack = ability.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            var abilityWeapon = abilityAttack.weapons[0];

            var popAndEffect = ability.GetBehavior<CreateEffectOnAbilityModel>().effectModel.Duplicate();
            popAndEffect.lifespan /= 8f;

            var newWeapon = realWeapon.Duplicate();
            var weaponEffect = newWeapon.projectile.GetBehavior<CreateEffectOnExpireModel>();
            weaponEffect.assetId = "";
            weaponEffect.effectModel = popAndEffect;
            weaponEffect.effectModel.scale /= 3f;
            weaponEffect.effectModel.useCenterPosition = false;
            weaponEffect.effectModel.lifespan /= 2f;
            newWeapon.rate = 4f;

            var newProjectile = abilityWeapon.projectile;
            newProjectile.GetBehavior<AgeModel>().lifespanFrames = 1;
            newProjectile.radius = realWeapon.projectile.radius * 2;

            newProjectile.behaviors = newProjectile.behaviors.RemoveItemOfType<Model, ClearHitBloonsModel>();

            newWeapon.projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile = newProjectile;
            newWeapon.projectile.behaviors = newWeapon.projectile.behaviors
                .RemoveItemOfType<Model, CreateEffectOnExhaustFractionModel>();

            var sound = Game.instance.model.GetTower(TowerType.MortarMonkey, 5).GetWeapon().projectile
                .GetBehavior<CreateSoundOnProjectileExhaustModel>();
            newWeapon.projectile.behaviors = newWeapon.projectile.behaviors
                .RemoveItemOfType<Model, CreateSoundOnProjectileExhaustModel>();
            newWeapon.projectile.AddBehavior(sound);

            model.GetAttackModels()[0].AddWeapon(newWeapon);
        }

        public override void Apply2(TowerModel model)
        {
            var multiplier = Game.instance.model.GetTower(TowerType.MortarMonkey, 0, 4).GetDescendant<TurboModel>()
                .multiplier;
            model.GetWeapon().Rate *= multiplier;
        }
    }
}