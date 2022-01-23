﻿using Assets.Scripts.Models;
using Assets.Scripts.Simulation.Towers.Behaviors;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Harmony;
using MelonLoader;
using UnityEngine;
using Main = TempleSacrificeHelper.Main;

[assembly: MelonInfo(typeof(Main), "Sacrifice Helper", "2.1.2", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TempleSacrificeHelper
{
    public class Main : BloonsTD6Mod
    {
        public const string SunTemple = "Sun Temple";
        public const string TrueSunGod = "True Sun God";

        public override string MelonInfoCsURL =>
            "https://raw.githubusercontent.com/doombubbles/BTD6-Mods/main/TempleSacrificeHelper/Main.cs";

        public override string LatestURL =>
            "https://github.com/doombubbles/BTD6-Mods/blob/main/TempleSacrificeHelper/TempleSacrificeHelper.dll?raw=true";

        public static readonly ModSettingInt TempleAlternateCost = new ModSettingInt(50000)
        {
            displayName = "Alternate Sun Temple Cost",
            minValue = 0
        };

        public static readonly ModSettingInt GodAlternateCost = new ModSettingInt(100000)
        {
            displayName = "Alternate True Sun God Cost",
            minValue = 0
        };

        public static readonly ModSettingInt MaxFromPops = new ModSettingInt(90000)
        {
            displayName = "Max Paragon Power From Pops\n(-1 for unlimited)",
            minValue = -1
        };

        public static readonly ModSettingInt MaxFromCash = new ModSettingInt(10000)
        {
            displayName = "Max Paragon Power From Cash (-1 for unlimited)",
            minValue = -1
        };

        public static readonly ModSettingInt MaxFromNonTier5s = new ModSettingInt(10000)
        {
            displayName = "Max Paragon Power From Non Tier 5s (-1 for unlimited)",
            minValue = -1
        };

        public static readonly ModSettingInt MaxFromTier5s = new ModSettingInt(90000)
        {
            displayName = "Max Paragon Power From Tier 5s (-1 for unlimited)",
            minValue = -1
        };

        private static readonly ModSettingInt PopsPerPoint = new ModSettingInt(180)
        {
            displayName = "Pops per Point of Paragon Power",
            minValue = 0
        };

        private static readonly ModSettingInt CashPerPoint = new ModSettingInt(25)
        {
            displayName = "Cash per Point of Paragon Power",
            minValue = 0
        };

        private static readonly ModSettingInt NonTier5sScaleFactor = new ModSettingInt(100)
        {
            displayName = "Paragon Power Scale Factor for Non Tier 5s",
            minValue = 0
        };

        private static readonly ModSettingInt Tier5sScaleFactor = new ModSettingInt(10000)
        {
            displayName = "Paragon Power Scale Factor for Tier 5s",
            minValue = 0
        };

        public static bool templeSacrificesOff = false;

        public override void OnNewGameModel(GameModel result)
        {
            result.paragonDegreeDataModel.maxPowerFromPops = MaxFromPops;
            if (result.paragonDegreeDataModel.maxPowerFromPops < 0)
            {
                result.paragonDegreeDataModel.maxPowerFromPops = int.MaxValue;
            }

            result.paragonDegreeDataModel.maxPowerFromMoneySpent = MaxFromCash;
            if (result.paragonDegreeDataModel.maxPowerFromMoneySpent < 0)
            {
                result.paragonDegreeDataModel.maxPowerFromMoneySpent = int.MaxValue;
            }

            result.paragonDegreeDataModel.maxPowerFromNonTier5Count = MaxFromNonTier5s;
            if (result.paragonDegreeDataModel.maxPowerFromNonTier5Count < 0)
            {
                result.paragonDegreeDataModel.maxPowerFromNonTier5Count = int.MaxValue;
            }

            result.paragonDegreeDataModel.maxPowerFromTier5Count = MaxFromTier5s;
            if (result.paragonDegreeDataModel.maxPowerFromTier5Count < 0)
            {
                result.paragonDegreeDataModel.maxPowerFromTier5Count = int.MaxValue;
            }

            result.paragonDegreeDataModel.popsOverX = PopsPerPoint;
            result.paragonDegreeDataModel.moneySpentOverX = CashPerPoint;
            result.paragonDegreeDataModel.nonTier5TowersMultByX = NonTier5sScaleFactor;
            result.paragonDegreeDataModel.tier5TowersMultByX = Tier5sScaleFactor;
        }

        public override void OnGameObjectsReset()
        {
            if (TSMThemeChanges.templeText != null)
            {
                foreach (var (_, text) in TSMThemeChanges.templeText)
                {
                    Object.Destroy(text);
                }

                TSMThemeChanges.templeText = null;
            }


            if (TSMThemeChanges.templeIcons != null)
            {
                foreach (var (_, icon) in TSMThemeChanges.templeIcons)
                {
                    Object.Destroy(icon);
                }

                TSMThemeChanges.templeIcons = null;
            }

            if (TSMThemeChanges.templeInfoButton != null)
            {
                Object.Destroy(TSMThemeChanges.templeInfoButton);
                TSMThemeChanges.templeInfoButton = null;
            }

            if (TSMThemeChanges.paragonButton != null)
            {
                Object.Destroy(TSMThemeChanges.paragonButton);
                TSMThemeChanges.paragonButton = null;
            }

            if (TSMThemeChanges.paragonButtonText != null)
            {
                Object.Destroy(TSMThemeChanges.paragonButtonText);
                TSMThemeChanges.paragonButtonText = null;
            }

            if (TSMThemeChanges.paragonText != null)
            {
                foreach (var (_, text) in TSMThemeChanges.paragonText)
                {
                    Object.Destroy(text);
                }

                TSMThemeChanges.paragonText = null;
            }


            if (TSMThemeChanges.paragonIcons != null)
            {
                foreach (var (_, icon) in TSMThemeChanges.paragonIcons)
                {
                    Object.Destroy(icon);
                }

                TSMThemeChanges.paragonIcons = null;
            }
        }

        [HarmonyPatch(typeof(MonkeyTemple), nameof(MonkeyTemple.StartSacrifice))]
        public class MonkeyTemple_StartSacrifice
        {
            [HarmonyPrefix]
            public static bool Prefix()
            {
                return !templeSacrificesOff;
            }
        }
    }
}