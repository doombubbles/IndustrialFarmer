using System.Collections.Generic;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper;
using MelonLoader;
using Main = AbilityChoice.Main;

[assembly: MelonInfo(typeof(Main), "Ability Choice", "2.0.0", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace AbilityChoice
{
    public class Main : BloonsTD6Mod
    {
        public static Dictionary<int, int> CurrentBoostIDs = new Dictionary<int, int>();

        public override string MelonInfoCsURL =>
            "https://raw.githubusercontent.com/doombubbles/BTD6-Mods/main/AbilityChoice/Main.cs";

        public override string LatestURL =>
            "https://github.com/doombubbles/BTD6-Mods/blob/main/AbilityChoice/AbilityChoice.dll?raw=true";

        public override void OnMainMenu()
        {
            ResetCaches();
        }

        public override void OnRestart()
        {
            ResetCaches();
        }

        public override void OnUpdate()
        {
            OverclockHandler.OnUpdate();
        }

        public void ResetCaches()
        {
            if (InGame.instance == null || !InGame.instance.quitting)
            {
                CurrentBoostIDs = new Dictionary<int, int>();
            }
        }

        public static MelonPreferences_Category AbilityChoiceSettings { get; private set; }

        public override void OnApplicationStart()
        {
            AbilityChoiceSettings = MelonPreferences.CreateCategory("AbilityChoiceSettings");
        }
    }
}