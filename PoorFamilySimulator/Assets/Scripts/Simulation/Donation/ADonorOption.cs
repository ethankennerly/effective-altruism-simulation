using System;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public class ADonorOption
    {
        public float Cost;
        public bool WillSelectNext;
        public bool WillFund;
        public float FundingProgress;
        public bool Funded;
    }
}
