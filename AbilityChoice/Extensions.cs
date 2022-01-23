using Assets.Scripts.Unity.UI_New.Upgrade;

namespace AbilityChoice
{
    public static class Extensions
    {
        public static ModAbilityChoice AbilityChoice(this UpgradeDetails upgradeDetails)
        {
            var name = upgradeDetails.upgrade?.name ?? "";
            ModAbilityChoice.Cache.TryGetValue(name, out var abilityChoice);
            return abilityChoice;
        }
    }
}