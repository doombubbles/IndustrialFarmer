﻿using System.Linq;
using Assets.Scripts.Models;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using MelonLoader;

[assembly: MelonInfo(typeof(BetterEziliTotem.Main), "Better Ezili Totem", "1.0.3", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace BetterEziliTotem
{
    public class Main : BloonsTD6Mod
    {
        public override string MelonInfoCsURL =>
            "https://raw.githubusercontent.com/doombubbles/BTD6-Mods/main/BetterEziliTotem/Main.cs";

        public override string LatestURL =>
            "https://github.com/doombubbles/BTD6-Mods/blob/main/BetterEziliTotem/BetterEziliTotem.dll?raw=true";

        private static readonly ModSettingInt AbilityCooldown = new ModSettingInt(5399)
        {
            displayName = "Totem Ability Cooldown",
            min = 0
        };

        public override void OnNewGameModel(GameModel gameModel)
        {
            for (var i = 7; i <= 20; i++)
            {
                var towerModel = gameModel.GetTowerFromId("Ezili " + i);
                var ability = towerModel.GetAbilities().First(b => b.name.Contains("Totem"));

                ability.livesCost = 0;
                ability.cooldownFrames = AbilityCooldown;
            }
        }
    }

}