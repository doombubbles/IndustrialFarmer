using Assets.Scripts.Unity.UI_New.InGame.RightMenu;
using Assets.Scripts.Unity.UI_New.InGame.StoreMenu;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace AbilityChoice.Patches
{
    /*[HarmonyPatch(typeof(ShopMenu), nameof(ShopMenu.Awake))]
    internal static class ShopMenu_Awake
    {
        [HarmonyPostfix]
        private static void Postfix(ShopMenu __instance)
        {
            var gridLayoutGroup = __instance.transform.GetComponentInChildren<GridLayoutGroup>();

            gridLayoutGroup.constraintCount = 3;
            gridLayoutGroup.cellSize = new Vector2(172, 204);
        }
    }*/
}