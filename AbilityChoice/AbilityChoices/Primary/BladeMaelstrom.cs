using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class BladeMaelstrom : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.BladeMaelstrom;
        protected override string Description1 => "Shoots out a slow swirl of global, high pierce blades.";

        protected override string Description2 =>
            "Blades have additional range range and pierce, and seek out Bloons on their own.";

        protected virtual int Pierce => 6;
        protected virtual float Lifespan => 4;

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);

            var activateAttackModel = abilityModel.GetBehavior<ActivateAttackModel>();
            var newAttack = activateAttackModel.attacks[0].Duplicate();

            var uptime = activateAttackModel.Lifespan / abilityModel.Cooldown;
            newAttack.weapons[0].Rate /= uptime;
            newAttack.GetDescendant<SpinModel>().rotationPerFrame *= uptime;
            newAttack.GetDescendant<SpinModel>().rotationPerSecond *= uptime;

            model.AddBehavior(newAttack);
        }

        public override void Apply2(TowerModel model)
        {
            model.range += 9;
            model.GetAttackModel().range += 9;
            
            var neva = Game.instance.model.GetTower(TowerType.MonkeyAce, 0, 0, 3);
            var behavior = neva.GetDescendant<TrackTargetModel>().Duplicate();

            behavior.TurnRate *= 3;
            behavior.constantlyAquireNewTarget = true;
            behavior.useLifetimeAsDistance = true;

            var weaponProjectile = model.GetWeapon().projectile;
            weaponProjectile.AddBehavior(behavior);
            weaponProjectile.pierce += Pierce;
            weaponProjectile.GetBehavior<TravelStraitModel>().Lifespan *= Lifespan;
        }
    }
}