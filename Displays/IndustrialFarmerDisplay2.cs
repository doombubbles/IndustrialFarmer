using Assets.Scripts.Models.Towers;

namespace IndustrialFarmer.Displays
{
    public class IndustrialFarmerDisplay2 : IndustrialFarmerDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.EngineerMonkey, 0, 1);
        
        public override bool UseForTower(int[] tiers)
        {
            return 3 <= tiers[0] && tiers[0] < 7;
        }
    }
}