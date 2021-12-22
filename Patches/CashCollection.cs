using System.Linq;
using Assets.Scripts.Simulation.Towers.Behaviors;
using Assets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;

namespace IndustrialFarmer.Patches
{
    public class CashCollection
    {
        private static readonly string[] CrateGUIDs =
            {"88442e0b3684e3446aaa70a036da69c9", "0d60d713eef3d3043915b89b35b04670"};

        private static float bananaMultiplier = 1f;
        private static float bananaCrateMultiplier = 1f;

        [HarmonyPatch(typeof(CollectCashZone), nameof(CollectCashZone.Process))]
        internal class CollectCashZone_Process
        {
            [HarmonyPrefix]
            internal static void Prefix(CollectCashZone __instance)
            {
                var name = __instance.collectCashZoneModel.name;
                if (name.Contains(ModContent.UpgradeID<Levels.Level2>()))
                {
                    bananaMultiplier = 1.1f;
                    bananaCrateMultiplier = 1.1f;
                }
                else if (name.Contains(ModContent.UpgradeID<Levels.Level9>()))
                {
                    bananaMultiplier = 1.15f;
                    bananaCrateMultiplier = 1.15f;
                }
                else if (name.Contains(ModContent.UpgradeID<Levels.Level13>()))
                {
                    bananaMultiplier = 1.15f;
                    bananaCrateMultiplier = 1.2f;
                }
                else if (name.Contains(ModContent.UpgradeID<Levels.Level19>()))
                {
                    bananaMultiplier = 1.15f;
                    bananaCrateMultiplier = 1.25f;
                }
                else
                {
                    bananaMultiplier = 1f;
                    bananaCrateMultiplier = 1f;
                }
            }

            [HarmonyPostfix]
            internal static void Postfix(CollectCashZone __instance)
            {
                bananaMultiplier = 1f;
                bananaCrateMultiplier = 1f;
            }
        }

        [HarmonyPatch(typeof(Cash), nameof(Cash.Pickup))]
        internal class Cash_Pickup
        {
            [HarmonyPrefix]
            internal static bool Prefix(Cash __instance)
            {
                var cashModel = __instance.cashModel.Duplicate();
                if (CrateGUIDs.Contains(__instance.projectile.display.displayModel.display))
                {
                    cashModel.maximum *= bananaCrateMultiplier;
                    cashModel.minimum *= bananaCrateMultiplier;
                }
                else
                {
                    cashModel.maximum *= bananaMultiplier;
                    cashModel.minimum *= bananaMultiplier;
                }

                __instance.UpdatedModel(cashModel);
                return true;
            }
        }
    }
}