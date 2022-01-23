using System.Linq;
using Assets.Scripts.Models.Effects;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Military
{
    public class FirstStrikeCapability : ModAbilityChoice
    {
        private const float ExpectedPierce = 10;
        private const float Factor = 50;

        public override string UpgradeId => UpgradeType.FirstStrikeCapability;

        protected override string Description1 => "Gains frequent First Strike style missile attacks.";

        protected override string Description2 =>
            "Ballistic missiles do more damage, further increased against MOABs and Ceramics";


        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            
            var abilityAttack = abilityModel.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            var abilityWeapon = abilityAttack.weapons[0];
            
            abilityWeapon.rate = abilityModel.Cooldown / Factor;

            foreach (var createProjectileOnExpireModel in abilityWeapon.projectile
                         .GetBehaviors<CreateProjectileOnExpireModel>())
            {
                createProjectileOnExpireModel.projectile.GetDamageModel().damage /= Factor;
                if (createProjectileOnExpireModel.projectile.radius > 10)
                {
                    createProjectileOnExpireModel.projectile.radius /= 2f;
                }
            }

            var asset = abilityWeapon.projectile.GetBehavior<CreateEffectOnExpireModel>();
            asset.assetId = "";
            asset.effectModel = new EffectModel(asset.name, asset.assetId, .5f, asset.lifespan, false,
                false, false, false, false, false, false);

            model.AddBehavior(abilityAttack);
        }

        public override void Apply2(TowerModel model)
        {
            var abilityModel = AbilityModel(model);

            var ballisticDamage = model.GetAttackModel("BallisticMissile").GetDescendant<DamageModel>();
            var baseWeaponRate = model.GetAttackModel("BallisticMissile").weapons[0].Rate;

            var descendants = abilityModel.GetDescendants<CreateProjectileOnExpireModel>().ToList();
            var abilityDamage = descendants.First(expireModel => expireModel.name.Contains("SingleTarget")).projectile
                .GetDamageModel();
            var addedMoabDps = abilityDamage.damage / abilityModel.Cooldown;

            model.GetAttackModel(1).GetDescendants<DamageModifierForTagModel>().ForEach(tagModel =>
            {
                var tagModelDamageAddative = addedMoabDps * baseWeaponRate / ExpectedPierce;
                //MelonLogger.Msg("tag: " + tagModelDamageAddative);
                tagModel.damageAddative += tagModelDamageAddative;
            });


            var splash = descendants.First(expireModel => expireModel.name.Contains("Explosion")).projectile;
            var addedSplashDps = splash.pierce * splash.GetDamageModel().damage / abilityModel.Cooldown;
            var ballisticDamageDamage = addedSplashDps * baseWeaponRate / ExpectedPierce;
            ballisticDamage.damage += ballisticDamageDamage;
            //MelonLogger.Msg("other: " + ballisticDamageDamage);
        }
    }
}