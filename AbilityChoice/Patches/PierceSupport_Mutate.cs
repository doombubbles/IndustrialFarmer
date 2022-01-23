using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Simulation.Towers.Behaviors;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using MelonLoader;

namespace AbilityChoice.Patches
{
    [HarmonyPatch(typeof(PierceSupport.MutatorTower), nameof(PierceSupport.MutatorTower.Mutate))]
    internal static class PierceSupport_Mutate
    {
        [HarmonyPrefix]
        internal static bool Prefix(PierceSupport.MutatorTower __instance, Model model, ref bool __result)
        {
            if (__instance.parent.pierceSupportModel.name.Contains("MultPierceSupportModel"))
            {
                var mult = __instance.parent.pierceSupportModel.pierce;
                model.GetDescendants<ProjectileModel>().ForEach(projectileModel => projectileModel.pierce *= mult);
                __result = true;
                return false;
            }

            return true;
        }
    }
}