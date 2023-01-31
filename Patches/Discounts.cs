using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Input;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity.Bridge;
using BuffQuery = Il2CppAssets.Scripts.Simulation.Towers.Buffs.BuffQuery;

namespace IndustrialFarmer.Patches;

public class Discounts
{
    public const string IndustrialFarmerDiscount = "IndustrialFarmerDiscount";

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


    [HarmonyPatch(typeof(TowerInventory), nameof(TowerInventory.GetTowerDiscount))]
    internal class TowerInventory_GetTowerDiscount
    {
        [HarmonyPrefix]
        internal static void Prefix(TowerModel def)
        {
            discountingTower = def;
        }
    }

    [HarmonyPatch(typeof(TowerManager), nameof(TowerManager.GetZoneDiscount))]
    internal class TowerManager_GetZoneDiscount
    {
        [HarmonyPostfix]
        internal static void Postfix(TowerManager __instance,
            ref Il2CppSystem.Collections.Generic.Dictionary<string, Il2CppSystem.Collections.Generic.List<DiscountZone>>
                __result)
        {
            if (__result != null &&
                discountingTower != null &&
                discountingTower.baseId != TowerType.BananaFarm)
            {
                if (__result.ContainsKey(IndustrialFarmerDiscount))
                {
                    __result[IndustrialFarmerDiscount].Clear();
                }
            }

            discountingTower = null;
        }
    }


    [HarmonyPatch(typeof(Tower), nameof(Tower.AddDiscountZoneBuffs))]
    internal class Tower_AddDiscountZoneBuffs
    {
        [HarmonyPostfix]
        internal static void Postfix(Tower __instance,
            ref Il2CppSystem.Collections.Generic.List<BuffQuery> __result)
        {
            if (__result != null && __instance.towerModel.baseId != TowerType.BananaFarm)
            {
                foreach (var buffQuery in __result)
                {
                    if (buffQuery != null && buffQuery.buffIndicator.name.Contains(nameof(IndustrialFarmer)))
                    {
                        buffQuery.canCurrentlyBuff = false;
                    }
                }
            }
        }
    }
}