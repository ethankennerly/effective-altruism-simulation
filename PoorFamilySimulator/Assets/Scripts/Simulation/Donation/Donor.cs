using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace PoorFamily.Simulation.Donation
{
    [Serializable]
    public sealed class Donor
    {
        [Range(0f, 100000f)] public float DisposableIncome = 30000f;
        public string Culture = "en-US";
        public float FundsAvailable;
        public string FundsAvailableString;

        public DonorOptionMenu OptionMenu = new DonorOptionMenu();

        public readonly List<ADonorOption> Options;

        private readonly CultureInfo m_CultureInfo;

        public Donor()
        {
            m_CultureInfo = CultureInfo.CreateSpecificCulture(Culture);

            Options = new List<ADonorOption>();
            Options.Add(OptionMenu.Save);
            Options.Add(OptionMenu.GiveDirectly);
        }

        private void SetCostStrings(List<ADonorOption> options)
        {
            foreach (ADonorOption option in options)
            {
                option.CostString = ConvertCurrencyToString(option.Cost);
            }
        }

        private string ConvertCurrencyToString(float currency)
        {
            int wholeCurrency = (int)currency;
            if (wholeCurrency == 0)
            {
                return "";
            }

            return wholeCurrency.ToString("C0", m_CultureInfo);
        }

        public void AddYears(float deltaYears)
        {
            FundsAvailable += deltaYears * DisposableIncome;

            SelectNextOption();
            TryFundOption();

            FundsAvailableString = ConvertCurrencyToString(FundsAvailable);
            SetCostStrings(Options);
        }

        private void SelectNextOption()
        {
            ADonorOption selectedOption = null;
            bool anyOptionSelected = false;
            foreach (ADonorOption option in Options)
            {
                anyOptionSelected = option.WillSelectNext;
                if (anyOptionSelected)
                {
                    selectedOption = option;
                    break;
                }
            }

            if (!anyOptionSelected)
            {
                return;
            }

            foreach (ADonorOption option in Options)
            {
                option.WillSelectNext = false;
                option.WillFund = option == selectedOption;
            }
        }

        private void TryFundOption()
        {
            foreach (ADonorOption option in Options)
            {
                if (!option.WillFund)
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
