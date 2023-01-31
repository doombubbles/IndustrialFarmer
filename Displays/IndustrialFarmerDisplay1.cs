using Il2CppAssets.Scripts.Models.Towers;

namespace IndustrialFarmer.Displays;

public class IndustrialFarmerDisplay1 : IndustrialFarmerDisplay
{
    public override string BaseDisplay => GetDisplay(TowerType.EngineerMonkey);

    public override bool UseForTower(int[] tiers)
    {
        return tiers[0] < 3;
    }
}