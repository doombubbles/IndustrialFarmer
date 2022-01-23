using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Mods;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

[assembly: MelonInfo(typeof(BetterArmorPiercing.Main), "Better Armor Piercing", "1.2.1", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BetterArmorPiercing
{
    public class Main : BloonsTD6Mod
    {
        public override string MelonInfoCsURL =>
            "https://raw.githubusercontent.com/doombubbles/BTD6-Mods/main/BetterArmorPiercing/Main.cs";

        public override string LatestURL =>
            "https://github.com/doombubbles/BTD6-Mods/blob/main/BetterArmorPiercing/BetterArmorPiercing.dll?raw=true";


        private static readonly ModSettingInt ArmorPiercingDartsCost = new ModSettingInt(3000)
        {
            displayName = "Armor Piercing Darts Cost",
            min = 0
        };

        private static readonly ModSettingInt HeatTippedDartsBonus = new ModSettingInt(1)
        {
            displayName = "Heat Tipped Darts Bonus Damage w/ Armor Piercing",
            min = 0,
            max = 10,
            slider = true
        };

        public override void OnNewGameModel(GameModel gameModel, List<ModModel> mods)
        {
            gameModel.GetUpgrade("Armor Piercing Darts").cost =
                CostHelper.CostForDifficulty(ArmorPiercingDartsCost, mods);

            foreach (var towerModel in gameModel.GetTowersWithBaseId(TowerType.MonkeySub)
                         .Where(model => model.appliedUpgrades.Contains("Armor Piercing Darts")))
            {
                var damageModel = towerModel.GetWeapon().projectile.GetDamageModel();
                damageModel.immuneBloonProperties = BloonProperties.None;
                if (towerModel.appliedUpgrades.Contains("Heat-tipped Darts"))
                {
                    damageModel.damage += HeatTippedDartsBonus;
                }
            }
        }
    }
}