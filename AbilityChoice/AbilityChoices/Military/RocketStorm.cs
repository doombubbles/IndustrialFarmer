using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using MelonLoader;

namespace AbilityChoice.AbilityChoices.Military
{
    public class RocketStorm : ModAbilityChoice
    {
        protected override string AbilityName => Name;  // There's no space in RocketStorm in the ability model /shrug

        public override string UpgradeId => UpgradeType.RocketStorm;
        
        protected override string Description1 => "Occasionally shoots a wave of Rocket Storm missiles.";
        protected override string Description2 => "Shoots a single stream of Rocket Storm missiles with the same accuracy of its main attack.";

        public override void Apply1(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var activateAttackModel = abilityModel.GetBehavior<ActivateAttackModel>();
            var abilityAttack = activateAttackModel.attacks[0].Duplicate();
            var uptime = activateAttackModel.Lifespan / abilityModel.Cooldown;

            var abilityWeapon = abilityAttack.weapons[0];
            
            abilityWeapon.rate /= uptime;
            

            model.GetAttackModels()[0].AddWeapon(abilityWeapon);
        }

        public override void Apply2(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            var activateAttackModel = abilityModel.GetBehavior<ActivateAttackModel>();
            var abilityAttack = activateAttackModel.attacks[0].Duplicate();
            var uptime = activateAttackModel.Lifespan / abilityModel.Cooldown;

            var abilityWeapon = abilityAttack.weapons[0];
            
            
            var realWeapon = model.GetWeapon();
            var count = 1;
            if (abilityWeapon.emission.IsType(out RandomEmissionModel randomEmissionModel))
            {
                count = randomEmissionModel.count;
            } else if (abilityWeapon.emission.IsType(out EmissionWithOffsetsModel emissionWithOffsetsModel))
            {
                count = emissionWithOffsetsModel.projectileCount;
            }
            else
            {
                MelonLogger.Msg("Couldn't find count ?");
            }
            abilityWeapon.emission = realWeapon.emission;

            abilityWeapon.rate /= uptime * count / 2;
                

            if (abilityWeapon.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile
                .HasBehavior<SlowModel>())
            {
                abilityWeapon.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile
                    .GetBehavior<SlowModel>().lifespan *= uptime;
                abilityWeapon.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile
                    .GetBehavior<SlowModel>()
                    .dontRefreshDuration = true;
                abilityWeapon.projectile.GetBehavior<CreateProjectileOnBlockerCollideModel>().projectile
                    .GetBehavior<SlowModel>().lifespan *= uptime;
                abilityWeapon.projectile.GetBehavior<CreateProjectileOnBlockerCollideModel>().projectile
                    .GetBehavior<SlowModel>()
                    .dontRefreshDuration = true;
            }
                
            abilityWeapon.GetBehavior<EjectEffectModel>().effectModel.lifespan *= uptime;
            
            model.GetAttackModels()[0].AddWeapon(abilityWeapon);
        }
    }
}