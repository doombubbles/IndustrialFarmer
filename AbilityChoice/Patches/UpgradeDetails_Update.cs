using Assets.Scripts.Unity.UI_New.Upgrade;
using Assets.Scripts.Unity.Utils;
using Assets.Scripts.Utils;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace AbilityChoice.Patches
{
    [HarmonyPatch(typeof(UpgradeDetails), nameof(UpgradeDetails.Update))]
    internal static class UpgradeDetails_Update
    {
        [HarmonyPostfix]
        private static void Postfix(UpgradeDetails __instance)
        {
            ModAbilityChoice.HandleIcon(__instance, true);
        }
    }
}