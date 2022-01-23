using System.Linq;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class Snowstorm : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.Snowstorm;
        
        protected override string Description1 => "Cold aura can partially slows MOAB class bloons.";

        public override void Apply1(TowerModel model)
        {
            var realSlow = model.GetBehavior<SlowBloonsZoneModel>();

            var totem = Game.instance.model.GetTowerFromId("NaturesWardTotem");

            var slow = totem.GetBehaviors<SlowBloonsZoneModel>().First(b => !b.name.Contains("NonMoabs")).Duplicate();
            slow.zoneRadius = realSlow.zoneRadius;
            slow.bindRadiusToTowerRange = true;
            slow.radiusOffset = realSlow.radiusOffset;

            model.AddBehavior(slow);
        }
    }
}