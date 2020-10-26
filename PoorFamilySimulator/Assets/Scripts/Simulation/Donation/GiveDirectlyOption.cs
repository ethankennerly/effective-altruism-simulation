using System;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public sealed class GiveDirectlyOption : ADonorOption
    {
        public GiveDirectlyOption()
        {
            Cost = 1205f;
        }
    }
}
