using BTD_Mod_Helper.Api.Enums;

namespace AbilityChoice.AbilityChoices.Support
{
    public class CarpetOfSpikes : SpikeStorm
    {
        public override string UpgradeId => UpgradeType.CarpetOfSpikes;
        protected override string AbilityName => UpgradeType.SpikeStorm;

        protected override string Description1 =>
            "Regularly sets a carpet of spikes over the whole track alongside the stream.";
        
        protected override string Description2 =>
            "Regularly sets a carpet of spikes over the whole track alongside the accelerated production.";
        
    }
}