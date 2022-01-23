﻿using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Mods;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

[assembly: MelonInfo(typeof(BetterUltraVision.Main), "Better UltraVision", "1.1.3", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace BetterUltraVision
{
    public class Main : BloonsTD6Mod
    {
        public override string MelonInfoCsURL =>
            "https://raw.githubusercontent.com/doombubbles/BTD6-Mods/main/BetterUltraVision/Main.cs";

        public override string LatestURL =>
            "https://github.com/doombubbles/BTD6-Mods/blob/main/BetterUltraVision/BetterUltraVision.dll?raw=true";
        
        
        private static readonly ModSettingInt UltravisionRangeBonus = new ModSettingInt(6)
        {
            displayName = "Ultravision Range Bonus",
            min = 0
        };
        
        private static readonly ModSettingInt UltravisionCost = new ModSettingInt(1200)
        {
            displayName = "Ultravision Cost",
            min = 0
        };

        public override void OnNewGameModel(GameModel gameModel, List<ModModel> mods)
        {
            gameModel.GetUpgrade("Ultravision").cost = CostHelper.CostForDifficulty(UltravisionCost, mods);
            
            foreach (var towerModel in gameModel.towers)
            {
                if (towerModel.appliedUpgrades.Contains("Ultravision"))
                {
                    towerModel.range += UltravisionRangeBonus - 3;

                    foreach (var attackModel in towerModel.GetAttackModels())
                    {
                        attackModel.range += UltravisionRangeBonus - 3;
                    }
                }
            }
        }
    }
}