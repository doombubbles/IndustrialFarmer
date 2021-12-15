﻿using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using HarmonyLib;

namespace IndustrialFarmer.Patches
{
    /// <summary>
    ///     For some reason the MorphBloon behavior doesn't even use it's own bloonId parameter lol
    /// </summary>
    public class BloonMorphing
    {
        private static string bloonId;

        [HarmonyPatch(typeof(MorphBloon), nameof(MorphBloon.Collide))]
        internal class MorphBloon_Collide
        {
            [HarmonyPrefix]
            internal static void Prefix(MorphBloon __instance)
            {
                bloonId = __instance.morphBloonModel.bloonId;
            }


            [HarmonyPostfix]
            internal static void Postfix(MorphBloon __instance)
            {
                bloonId = null;
            }
        }

        [HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
        internal class Bloon_Damage
        {
            [HarmonyPrefix]
            internal static void Prefix(Bloon __instance, ref float totalAmount)
            {
                if (bloonId == "Green")
                {
                    totalAmount -= 2;
                }
            }
        }


        [HarmonyPatch(typeof(CosmeticHelper), nameof(CosmeticHelper.GetBloonModel))]
        internal class CosmeticHelper_GetBloonModel
        {
            [HarmonyPrefix]
            internal static void Postfix(int emissionIndex, ref string id)
            {
                if (bloonId != null)
                {
                    id = bloonId;
                }
            }
        }
    }
}