using System.Linq;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.TowerFilters;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using UnhollowerBaseLib;

namespace AbilityChoice.AbilityChoices.Primary
{
    public class AbsoluteZero : Snowstorm
    {
        public override string UpgradeId => UpgradeType.AbsoluteZero;
        protected override string Description1 => "Cold Aura slows MOABs even further. Also globally buffs the attack speed of Ice Monkeys.";

        public override void Apply1(TowerModel model)
        {
            base.Apply1(model);

            model.GetBehaviors<SlowBloonsZoneModel>().Last().speedScale -= .1f;
            
            var abilityModel = AbilityModel(model);
            var support = abilityModel.GetBehavior<ActivateRateSupportZoneModel>();

            var avgBuff = CalcAvgBonus(support.lifespan / abilityModel.Cooldown, 1 / support.rateModifier);

            var buff = new RateSupportModel("RateSupportZoneModel_AbilityChoice", 1 / avgBuff, true,
                "AbsoluteZeroRateBuff2", true, 1,
                new Il2CppReferenceArray<TowerFilterModel>(
                    new TowerFilterModel[]
                    {
                        new FilterInBaseTowerIdModel("FilterInBaseTowerIdModel_",
                            new Il2CppStringArray(new[] {TowerType.IceMonkey}))
                    }
                )
                , "", "")
            {
                showBuffIcon = false,
                appliesToOwningTower = true
            };
            model.AddBehavior(buff);
        }
    }
}