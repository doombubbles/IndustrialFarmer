using Assets.Scripts.Models.Towers;
using Assets.Scripts.Simulation.Towers.Behaviors;
using Assets.Scripts.Simulation.Track;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;

namespace IndustrialFarmer.Patches
{
    public class WaterFreezing
    {
        private static TowerModel currentTowerModel;


        [HarmonyPatch(typeof(Map), nameof(Map.ZoneAllowsPlacement))]
        internal class Map_ZoneAllowsPlacement
        {
            [HarmonyPrefix]
            internal static void Prefix(Map __instance, TowerModel tm)
            {
                currentTowerModel = tm;
            }

            [HarmonyPostfix]
            internal static void Postfix()
            {
                currentTowerModel = null;
            }
        }


        [HarmonyPatch(typeof(FreezeNearbyWater), nameof(FreezeNearbyWater.IsInZone))]
        internal class FreezeNearbyWater_IsInZone
        {
            [HarmonyPostfix]
            internal static void Postfix(FreezeNearbyWater __instance, ref bool __result)
            {
                if (__instance.tower.towerModel?.GetModTower() is IndustrialFarmer &&
                    currentTowerModel != null &&
                    currentTowerModel.baseId != TowerType.BananaFarm)
                {
                    __result = false;
                }
            }
        }
    }
}