using Assets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api.Enums;

namespace AbilityChoice.AbilityChoices.Magic
{
    public class SpiritForest : JunglesBounty
    {
        public override string UpgradeId => UpgradeType.SpiritOfTheForest;

        protected override string Description1 =>
            "Grows thorned vines along the path that deal constant damage and bonus damage to ceramics. Vines nearest the Spirit of the Forest do more damage. Periodically gives lives as well as more cash.";

        protected override string Description2 =>
            "Grows thorned vines along the path that deal constant damage and bonus damage to ceramics. Vines nearest the Spirit of the Forest do more damage. Income bonus increased to 30%";
        

        protected override float Income => 1.3f;
    }
}