using BTD_Mod_Helper;
using MelonLoader;
using Main = IndustrialFarmer.Main;

[assembly: MelonInfo(typeof(Main), "Industrial Farmer", "1.0.0", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace IndustrialFarmer
{
    public class Main : BloonsTD6Mod
    {
        public override string MelonInfoCsURL =>
            "https://raw.githubusercontent.com/doombubbles/IndustrialFarmer/main/Main.cs";

        public override string LatestURL =>
            "https://github.com/doombubbles/IndustrialFarmer/blob/main/IndustrialFarmer.dll?raw=true";
    }
}