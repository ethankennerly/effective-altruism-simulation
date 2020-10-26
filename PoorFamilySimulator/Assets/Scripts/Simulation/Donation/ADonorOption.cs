using System;
using UnityEngine;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public class ADonorOption
    {
        public float Cost;
        public string CostString;
        public bool NoRoomForFunding;
        public bool WillSelectNext;
        public bool WillFund;
        [Range(0f, 1f)] public float FundingProgress;
        public bool Funded;

        public void Select()
        {
            WillSelectNext = true;
        }
    }
}
