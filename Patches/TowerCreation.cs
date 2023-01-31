using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Bridge;
using BTD_Mod_Helper.Api;

namespace IndustrialFarmer.Patches;

public class TowerCreation
{
    [HarmonyPatch(typeof(UnityToSimulation.CreateTowerAtAction), nameof(UnityToSimulation.CreateTowerAtAction.Run))]
    internal static class UnityToSimulation_CreateTowerAt
    {
        [HarmonyPostfix]
        private static void Postfix(UnityToSimulation.CreateTowerAtAction __instance, UnityToSimulation uts)
        {
            if (__instance.towerModelName.StartsWith(ModContent.GetInstance<IndustrialFarmer>().Id))
            {
                uts.Simulation.GetTowerInventory(__instance.initiatingPlayerNumber)
                    .AddFreeTowers(TowerType.BananaFarm, 1, "", 0);
            }
        }
    }
}