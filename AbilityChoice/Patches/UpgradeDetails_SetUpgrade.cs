using Assets.Scripts.Unity.UI_New.Upgrade;
using HarmonyLib;

namespace AbilityChoice.Patches
{
    [HarmonyPatch(typeof(UpgradeDetails), nameof(UpgradeDetails.SetUpgrade))]
    internal static class UpgradeDetails_SetUpgrade
    {
        [HarmonyPostfix]
        private static void Postfix(UpgradeDetails __instance)
        {
            ModAbilityChoice.HandleIcon(__instance);
        }
    }
}