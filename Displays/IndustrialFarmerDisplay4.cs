using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Extensions;

namespace IndustrialFarmer.Displays
{
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
}