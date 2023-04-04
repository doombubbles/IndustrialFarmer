using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Track;

namespace IndustrialFarmer.Patches;

public class WaterFreezing
{
    private static TowerModel? currentTowerModel;
    
    [HarmonyPatch(typeof(Map), nameof(Map.ZoneAllowsPlacement))]
    internal class Map_ZoneAllowsPlacement
    {
        [HarmonyPrefix]
        internal static void Prefix(IPlaceableEntity pe)
        {
            if (pe.Is(out TowerModel tm))
            {
                currentTowerModel = tm;
            }
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