using System;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public sealed class DonorOptionMenu
    {
        public SaveOption Save = new SaveOption();
        public GiveDirectlyOption GiveDirectly = new GiveDirectlyOption();
    }
}
