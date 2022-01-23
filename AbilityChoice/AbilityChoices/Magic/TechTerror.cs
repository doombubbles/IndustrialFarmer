using System.Linq;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Towers.Weapons.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class TechTerror : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.TechTerror;

        protected override string Description1 => "Frequently annihilates nearby Bloons.";
        protected override string Description2 => "Nanobot plasma seeks out and destroys Bloons with strong Crits";

        private const int Factor = 20;

        public override void Apply1(TowerModel model)
        {
            var ability = AbilityModel(model);
            var abilityAttack = ability.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            var abilityWeapon = abilityAttack.weapons[0];

            var effect = ability.GetBehavior<CreateEffectOnAbilityModel>().effectModel;
            abilityWeapon.projectile.display = effect.assetId;
            var effectBehavior =
                new CreateEffectOnExhaustFractionModel("CreateEffectOnExhaustFractionModel_Annihilation", "", effect, 0,
                    false, 1.0f, -1f, false);
            abilityWeapon.projectile.AddBehavior(effectBehavior);
            abilityWeapon.projectile.GetDamageModel().damage /= Factor;
            abilityWeapon.rate = ability.Cooldown / Factor;

            abilityAttack.range = abilityWeapon.projectile.radius - 10;
            abilityAttack.fireWithoutTarget = false;

            model.AddBehavior(abilityAttack);
        }

        public override void Apply2(TowerModel model)
        {
            model.GetDescendants<CritMultiplierModel>().ForEach(multiplierModel => { multiplierModel.damage *= 5; });

            var retarget = Game.instance.model.GetTower(TowerType.BoomerangMonkey, 4)
                .GetDescendant<RetargetOnContactModel>().Duplicate();

            retarget.expireIfNoTargetFound = false;

            model.GetDescendants<ProjectileModel>()
                .ToList()
                .Where(projectileModel => projectileModel.HasBehavior<DamageModel>())
                .Do(projectileModel => projectileModel.AddBehavior(retarget.Duplicate()));
        }
    }
}