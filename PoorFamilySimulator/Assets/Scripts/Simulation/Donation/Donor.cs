using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public sealed class Donor
    {
        [Range(0f, 100000f)] public float DisposableIncome = 30000f;
        public float FundsAvailable;
        public ADonorOption SelectedOption;

        public DonorOptionMenu OptionMenu = new DonorOptionMenu();

        public readonly List<ADonorOption> Options;

        public Donor()
        {
            Options = new List<ADonorOption>();
            Options.Add(OptionMenu.Save);
            Options.Add(OptionMenu.GiveDirectly);
        }

        public void Select(ADonorOption donorOption)
        {
            SelectedOption = donorOption;
        }

        public void AddYears(float deltaYears)
        {
            FundsAvailable += deltaYears * DisposableIncome;

            TryFundOption();
        }

        private void TryFundOption()
        {
            foreach(ADonorOption option in Options)
            {
                if (option != SelectedOption)
                {
                    option.FundingProgress = 0f;
                    option.Funded = false;
                    continue;
                }

                option.FundingProgress = FundsAvailable / option.Cost; 
                option.Funded = option.FundingProgress >= 1f;
                if (!option.Funded)
                {
                    continue;
                }

                FundsAvailable -= option.Cost;
            }
        }
    }
}
