using Assets.Scripts.Models.Towers;

namespace IndustrialFarmer.Displays
{
    public class IndustrialFarmerDisplay3 : IndustrialFarmerDisplay
    {
        public override string BaseDisplay => GetDisplay(TowerType.EngineerMonkey, 0, 2);
        
        public override bool UseForTower(int[] tiers)
        {
            return 7 <= tiers[0] && tiers[0] < 10;
        }
    }
}