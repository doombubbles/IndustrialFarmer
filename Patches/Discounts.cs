using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Input;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppSystem.Collections.Generic;
using BuffQuery = Il2CppAssets.Scripts.Simulation.Towers.Buffs.BuffQuery;

namespace IndustrialFarmer.Patches;

public static class Discounts
{
    private static TowerModel? discountingTower;


    [HarmonyPatch(typeof(TowerToSimulation), nameof(TowerToSimulation.GetUpgradeCost))]
    internal class TowerToSimulation_GetUpgradeCost
    {
        [HarmonyPrefix]
        internal static void Prefix(TowerToSimulation __instance)
        {
            discountingTower = __instance.Def;
        }
    }

    
    [HarmonyPatch(typeof(TowerInventory), nameof(TowerManager.GetTowerCost))]
    internal class TowerManager_GetTowerCost
    {
        [HarmonyPrefix]
        internal static void Prefix(TowerModel tower)
        {
            discountingTower = tower;
        }
    }

    [HarmonyPatch(typeof(TowerManager), nameof(TowerManager.GetZoneDiscount))]
    internal class TowerManager_GetZoneDiscount
    {
        [HarmonyPostfix]
        internal static void Postfix(ref Dictionary<string, List<DiscountZone>> __result)
        {
            if (__result != null &&
                discountingTower != null &&
                discountingTower.baseId != TowerType.BananaFarm)
            {
                if (__result.ContainsKey(IndustrialFarmer.IndustrialFarmerDiscount))
                {
                    __result[IndustrialFarmer.IndustrialFarmerDiscount].Clear();
                }
            }

            discountingTower = null;
        }
    }


    [HarmonyPatch(typeof(Tower), nameof(Tower.AddDiscountZoneBuffs))]
    internal class Tower_AddDiscountZoneBuffs
    {
        [HarmonyPostfix]
        internal static void Postfix(Tower __instance, ref IEnumerable<BuffQuery> __result)
        {
            if (__result == null || __instance.towerModel.baseId == TowerType.BananaFarm) return;

            foreach (var buffQuery in __result.ToList().Where(buffQuery =>
                         buffQuery != null && buffQuery.buffIndicator.name.Contains(nameof(IndustrialFarmer))))
            {
                buffQuery.canCurrentlyBuff = false;
            }
        }
    }
}