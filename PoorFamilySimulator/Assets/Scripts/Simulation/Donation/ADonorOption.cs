using System;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public abstract class ADonorOption
    {
        public float Cost;
        public bool Funded;
        public float FundingProgress;
    }
}
