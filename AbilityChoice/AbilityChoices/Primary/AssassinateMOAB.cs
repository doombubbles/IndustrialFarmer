using System.Linq;
using AbilityChoice.Displays;
using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class AssassinateMOAB : ModAbilityChoice
    {
        private const float ExpectedPierce = 4;
        private const float Factor = 10;

        public override string UpgradeId => UpgradeType.MOABAssassin;

        protected override string Description1 =>
            "Frequently shoots out mini Moab Assassin missiles at the strongest Moab on screen.";

        protected override string Description2 => "Main attacks do further increased MOAB damage with more range.";

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);

            var newAttack = abilityModel.GetDescendant<AttackModel>().Duplicate();

            var newAttackWeapon = newAttack.weapons[0];
            var damageModel = newAttackWeapon.projectile.GetDamageModel();
            damageModel.damage /= Factor;

            newAttackWeapon.Rate = abilityModel.Cooldown / Factor;

            ApplyDisplay(newAttackWeapon.projectile);

            newAttack.GetDescendant<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 1;

            model.AddBehavior(newAttack);
        }

        public override void Apply2(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var baseWeaponRate = BaseTowerModel.GetWeapon().Rate;

            var proj = model.GetWeapon().GetDescendant<CreateProjectileOnContactModel>().projectile;
            var moabDamage = proj.GetBehaviors<DamageModifierForTagModel>().First(m => m.tag == "Moabs");


            var abilityDamage = abilityModel.GetDescendant<WeaponModel>().projectile.GetDamageModel();
            var addedMoabDps = abilityDamage.damage / abilityModel.Cooldown;
            moabDamage.damageAddative += addedMoabDps * baseWeaponRate / ExpectedPierce;

            var splash = abilityModel.GetDescendant<CreateProjectileOnContactModel>().projectile;
            var addedSplashDps = splash.pierce * splash.GetDamageModel().damage / abilityModel.Cooldown;
            proj.GetDamageModel().damage += addedSplashDps * baseWeaponRate / ExpectedPierce;

            model.range += 5;
            model.GetDescendants<AttackModel>().ForEach(attackModel => attackModel.range += 5);
        }

        protected virtual void ApplyDisplay(ProjectileModel projectileModel)
        {
            projectileModel.ApplyDisplay<MiniMoabAssassin>();
        }
    }
}