using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api;

namespace IndustrialFarmer.Patches;

public class CashCollection
{
    private static readonly string[] CrateGUIDs =
        {"88442e0b3684e3446aaa70a036da69c9", "0d60d713eef3d3043915b89b35b04670"};

    private static float bananaBonus;
    private static float crateBonus;

    [HarmonyPatch(typeof(CollectCashZone), nameof(CollectCashZone.Process))]
    internal class CollectCashZone_Process
    {
        [HarmonyPrefix]
        internal static void Prefix(CollectCashZone __instance)
        {
            var name = __instance.collectCashZoneModel.name;
            if (name.Contains(ModContent.UpgradeID<Levels.Level2>()))
            {
                bananaBonus = 0.1f;
                crateBonus = 0.1f;
            }
            else if (name.Contains(ModContent.UpgradeID<Levels.Level9>()))
            {
                bananaBonus = 0.15f;
                crateBonus = 0.15f;
            }
            else if (name.Contains(ModContent.UpgradeID<Levels.Level13>()))
            {
                bananaBonus = 0.15f;
                crateBonus = 0.2f;
            }
            else if (name.Contains(ModContent.UpgradeID<Levels.Level19>()))
            {
                bananaBonus = 0.15f;
                crateBonus = 0.25f;
            }
            else
            {
                bananaBonus = 0f;
                crateBonus = 0f;
            }
        }

        [HarmonyPostfix]
        internal static void Postfix(CollectCashZone __instance)
        {
            bananaBonus = 0f;
            crateBonus = 0f;
        }
    }

    [HarmonyPatch(typeof(Cash), nameof(Cash.Pickup))]
    internal class Cash_Pickup
    {
        [HarmonyPrefix]
        internal static void Prefix(Cash __instance)
        {
            var cashModel = __instance.cashModel.Duplicate();
            if (CrateGUIDs.Contains(__instance.projectile.display.displayModel.display.guidRef))
            {
                cashModel.bonusMultiplier += crateBonus;
            }
            else
            {
                cashModel.bonusMultiplier += bananaBonus;
            }

            __instance.UpdatedModel(cashModel);
        }
    }
}