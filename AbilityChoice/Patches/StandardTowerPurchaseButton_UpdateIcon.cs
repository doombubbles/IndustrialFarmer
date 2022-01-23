using Assets.Scripts.Unity.UI_New.InGame.StoreMenu;
using HarmonyLib;
using UnityEngine;

namespace AbilityChoice.Patches
{
    /*[HarmonyPatch(typeof(StandardTowerPurchaseButton), nameof(StandardTowerPurchaseButton.UpdateIcon))]
    internal static class StandardTowerPurchaseButton_UpdateIcon
    {

        [HarmonyPostfix]
        private static void Postfix(StandardTowerPurchaseButton __instance)
        {
            __instance.icon.transform.localScale = new Vector3(.6f, .6f, .6f);
            __instance.towerCostText.transform.localScale = new Vector3(.8f, .8f, .8f);
            __instance.towerCountText.transform.localScale = new Vector3(.8f, .8f, .8f);
        }
    }*/
}