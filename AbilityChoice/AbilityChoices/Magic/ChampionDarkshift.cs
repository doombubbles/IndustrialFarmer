using Assets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class ChampionDarkshift : ModAbilityChoice
    {
        public override string UpgradeId => UpgradeType.DarkChampion;
        protected override string AbilityName => Name;

        protected override string Description1 => "Champion's dark blades excel at puncturing and ruining all Bloon types. Further increased range.";
        
        public override void Apply1(TowerModel model)
        {
            model.IncreaseRange(20);
        }
    }
}