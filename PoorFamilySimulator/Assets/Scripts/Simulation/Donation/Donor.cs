using System;
using UnityEngine;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public sealed class Donor
    {
        [Range(0f, 100000f)] public float DisposableIncome;
        public float FundsAvailable;

        public DonorOptionMenu OptionMenu = new DonorOptionMenu();

        public void Select(ADonorOption donorOption)
        {
            UnityEngine.Debug.Log("TODO: Select: " + donorOption);
        }

        public void AddYears(float deltaYears)
        {
            UnityEngine.Debug.Log("TODO: Add Years: " + deltaYears);
        }
    }
}
