using Assets.Scripts.Models.Towers;
using Assets.Scripts.Simulation.Input;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;

namespace IndustrialFarmer.Patches
{
    public class TowerCreation
    {
        [HarmonyPatch(typeof(InputManager), nameof(InputManager.CreateTowerAt))]
        internal class InputManager_CreateTowerAt
        {
            [HarmonyPostfix]
            internal static void Postfix(InputManager __instance, TowerModel towerModel)
            {
                if (towerModel.GetModTower() is IndustrialFarmer)
                {
                    __instance.Sim.GetTowerInventory(__instance.id).AddFreeTowers(TowerType.BananaFarm, 1, "", 0);
                }
            }
        }
    }
}