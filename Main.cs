global using HarmonyLib;
global using MelonLoader;
global using BTD_Mod_Helper.Extensions;
global using System.Linq;
using BTD_Mod_Helper;
using IndustrialFarmer;

[assembly: MelonInfo(typeof(IndustrialFarmerMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace IndustrialFarmer;

public class IndustrialFarmerMod : BloonsTD6Mod
{
}