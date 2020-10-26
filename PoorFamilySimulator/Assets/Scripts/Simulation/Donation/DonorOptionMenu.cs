using System;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public sealed class DonorOptionMenu
    {
        public GiveDirectlyOption GiveDirectly = new GiveDirectlyOption();
        public SaveOption Save = new SaveOption();
        public EatOutAndEntertainOption EatOutAndEntertain = new EatOutAndEntertainOption();
    }
}
