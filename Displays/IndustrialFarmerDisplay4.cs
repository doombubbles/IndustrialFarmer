using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Display;

namespace IndustrialFarmer.Displays;

public class IndustrialFarmerDisplay4 : IndustrialFarmerDisplay
{
    public override string BaseDisplay => GetDisplay(TowerType.EngineerMonkey, 3);

    public override bool UseForTower(int[] tiers)
    {
        return 10 <= tiers[0] && tiers[0] < 20;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        base.ModifyDisplayNode(node);
        node.RemoveBone("MonkeyRig:Propjectile_L");
    }
}