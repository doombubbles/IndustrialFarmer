using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.Upgrade;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using MelonLoader;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AbilityChoice.Patches
{
    [HarmonyPatch(typeof(Button), nameof(Button.OnPointerClick))]
    internal static class Button_OnPointerClick
    {
        [HarmonyPostfix]
        private static void Postfix(Button __instance, PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right &&
                __instance.gameObject.GetComponentInParent<UpgradeDetails>().IsType(out UpgradeDetails upgrade) 
                && upgrade.AbilityChoice() is ModAbilityChoice abilityChoice)
            {
                if (InGame.instance != null)
                {
                    MelonLogger.Warning("This change will only take effect when you reload the match");
                }
                abilityChoice.Toggle();
                upgrade.OnPointerExit(eventData);
                upgrade.OnPointerEnter(eventData);
            }
        }
    }
}