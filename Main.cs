using BTD_Mod_Helper;
using HarmonyLib;
using MelonLoader;
using Main = IndustrialFarmer.Main;

[assembly: MelonInfo(typeof(Main), "Industrial Farmer", "0.0.0", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonPriority(Priority.High)]

namespace IndustrialFarmer
{
    public class Main : BloonsTD6Mod
    {
    }
}