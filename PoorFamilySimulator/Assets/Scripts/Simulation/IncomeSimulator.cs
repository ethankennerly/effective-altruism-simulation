using PoorFamily.Simulation.Donation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class IncomeSimulator
    {
        private const int kDoublingBase = 2;

        [Header("Satisfaction")]
        [Range(0, 10)] public float AverageDoublingsOfIncome;
        [Range(0f, 1f)] public float NormalizedIncome;
        public int NormalizedIncomePercent;
        public string NormalizedIncomeString = "0%";

        [Range(100, 102400)] public float PoorestIncome = 100f;
        [Range(100, 102400)] public float RichestIncome = 102400f;

        public float PoorestDoublings;
        [Range(0, 10)] public float RichestDoublings;

        [Header("Raise")]
        public float MinAge = 10f;
        public float RaisePerYear = 30f;
        public float PeakAge = 30f;
        public float RaisePerYearAfterPeak = -20f;

        private readonly List<Human> m_Humans;
        private int m_NumHumans;
        private readonly Donor m_Donor;

        public IncomeSimulator(List<Human> humans, Donor donor = null)
        {
            m_Humans = humans;
            m_Donor = donor;
        }

        public void AddYears(float deltaYears)
        {
            m_NumHumans = m_Humans.Count;
            TryGiveDirectly(m_Donor);
            CalculateRaise(deltaYears);
            TransferBySchedule(deltaYears);
            TryShareIncome();
            NormalizeIncome();
        }

        private void CalculateRaise(float deltaYears)
        {
            float deltaIncome = RaisePerYear * deltaYears;
            float deltaIncomeAfterPeak = RaisePerYearAfterPeak * deltaYears;
            foreach (Human human in m_Humans)
            {
                if (human.Age <= MinAge)
                {
                    continue;
                }

                if (human.Age > PeakAge)
                {
                    human.Income += deltaIncomeAfterPeak;
                    continue;
                }

                human.Income += deltaIncome;
            }
        }

        #region Scheduling Transfers

        private List<float> m_NextAdditionInFutureYears;

        public bool IsTransferPending()
        {
            return m_NextAdditionInFutureYears != null;
        }

        private void TryGiveDirectly(Donor donor)
        {
            if (donor == null)
            {
                return;
            }

            ADonorOption giveDirectlyOption = donor.OptionMenu.GiveDirectly;
            if (!giveDirectlyOption.Funded)
            {
                return;
            }

            GiveDirectly(giveDirectlyOption.Cost);
        }

        public void GiveDirectly(float cost)
        {
            List<float> giveDirectlySchedule = BuildGiveDirectlySchedule(cost);
            ScheduleTransfer(giveDirectlySchedule);
        }

        public List<float> BuildGiveDirectlySchedule(float cost)
        {
            float recipientRate = 0.83f;
            float payout = cost * recipientRate;
            float investRate = 0.39f;
            float yearsOfInvestment = 10f;

            List<float> additionInFutureYears = new List<float>((int)yearsOfInvestment);
            float firstYear = (1f - investRate) * payout;
            additionInFutureYears.Add(firstYear);

            float returnPerYear = investRate * payout / yearsOfInvestment;
            for (int yearIndex = 0; yearIndex < yearsOfInvestment; ++yearIndex)
            {
                additionInFutureYears.Add(returnPerYear);
            }

            return additionInFutureYears;
        }

        /// <summary>
        /// This adds to any transfer not applied yet.
        /// </summary>
        public void ScheduleTransfer(List<float> additionInFutureYears)
        {
            AddTransfers(ref m_NextAdditionInFutureYears, additionInFutureYears);
        }

        private float m_RemainingYearsUntilNextTransfer = 0.5f;

        private void TransferBySchedule(float deltaYears)
        {
            for (m_RemainingYearsUntilNextTransfer -= deltaYears;
                m_RemainingYearsUntilNextTransfer <= 0f;
                ++m_RemainingYearsUntilNextTransfer)
            {
                TransferNextYear();
            }
        }

        private void TransferNextYear()
        {
            foreach (Human human in m_Humans)
            {
                if (human.ScheduledTransfers == null || human.ScheduledTransfers.Count == 0)
                {
                    continue;
                }

                float previousAddition = human.ScheduledTransfers[0];
                human.Income -= previousAddition;
                human.ScheduledTransfers.RemoveAt(0);
                if (human.ScheduledTransfers.Count == 0)
                {
                    continue;
                }

                float nextAddition = human.ScheduledTransfers[0];
                human.Income += nextAddition;
            }

            DistributeTransfers(m_NextAdditionInFutureYears);
            m_NextAdditionInFutureYears = null;
        }

        private void DistributeTransfers(List<float> additionInFutureYears)
        {
            if (additionInFutureYears == null || additionInFutureYears.Count == 0)
            {
                return;
            }

            if (m_NumHumans == 0)
            {
                return;
            }

            int numTransfers = additionInFutureYears.Count;
            List<float> averageTransfers = new List<float>(numTransfers);
            for (int transferIndex = 0; transferIndex < numTransfers; ++transferIndex)
            {
                float averageTransfer = additionInFutureYears[transferIndex] / m_NumHumans;
                averageTransfers.Add(averageTransfer);
            }

            foreach (Human human in m_Humans)
            {
                AddTransfers(ref human.ScheduledTransfers, averageTransfers);

                human.Income += human.ScheduledTransfers[0];
            }
        }

        private static void AddTransfers(
            ref List<float> originalTransfers, 
            List<float> additionalTransfers)
        {
            int numTransfers = additionalTransfers.Count;
            if (originalTransfers == null)
            {
                originalTransfers = new List<float>(numTransfers);
            }

            
            int yearIndex = 0;
            for (int numOriginalTransfers = originalTransfers.Count;
                yearIndex < numOriginalTransfers;
                ++yearIndex)
            {
                originalTransfers[yearIndex] += additionalTransfers[yearIndex];
            }

            for (; yearIndex < numTransfers; ++yearIndex)
            {
                originalTransfers.Add(additionalTransfers[yearIndex]);
            }
        }

        #endregion Scheduling Transfers

        #region Income Sharing

        [Header("Income Sharing")]
        public bool SharingEnabled;

        private void TryShareIncome()
        {
            if (!SharingEnabled)
            {
                return;
            }

            float sumOfIncome = 0f;
            foreach (Human human in m_Humans)
            {
                sumOfIncome += human.Income;
            }

            float averageIncome = sumOfIncome / m_NumHumans;
            foreach (Human human in m_Humans)
            {
                human.Income = averageIncome;
            }
        }

        #endregion Income Sharing

        #region Normalized Income

        private void NormalizeIncome()
        {
            AverageDoublingsOfIncome = CalculateAverageDoublingsOfIncome();
            NormalizedIncome = Mathf.Clamp01(AverageDoublingsOfIncome / RichestDoublings);

            int nextNormalizedIncomePercent = (int)(NormalizedIncome * 100);
            if (NormalizedIncomePercent == nextNormalizedIncomePercent)
            {
                return;
            }

            NormalizedIncomePercent = nextNormalizedIncomePercent;
            NormalizedIncomeString = NormalizedIncomePercent + "%";
        }

        private float CalculateAverageDoublingsOfIncome()
        {
            CalculateNormalizedIncome();

            if (m_NumHumans == 0)
            {
                return 0f;
            }

            float sum = 0f;
            foreach (Human human in m_Humans)
            {
                sum += human.DoublingsOfIncome;
            }

            float averageWealth = sum / m_NumHumans;
            return averageWealth;
        }

        private void CalculateNormalizedIncome()
        {
            PoorestDoublings = Mathf.Log(PoorestIncome, kDoublingBase);
            RichestDoublings = Mathf.Log(RichestIncome, kDoublingBase) - PoorestDoublings;

            foreach (Human human in m_Humans)
            {
                human.DoublingsOfIncome = human.Income <= 1f ?
                    0f :
                    Mathf.Log(human.Income, kDoublingBase) - PoorestDoublings;
                human.NormalizedIncome = Mathf.Clamp01(human.DoublingsOfIncome / RichestDoublings);
            }
        }

        #endregion Normalized Income

        #region Life Expectancy

        [Header("Life Expectancy")]
        public float LifeExpectancyAtPoorestIncome = 47f;
        public float LifeExpectancyPerDoublingOfIncome = 4f;

        public void CalculateLifeExpectancy()
        {
            foreach (Human human in m_Humans)
            {
                human.LifeExpectancy = LifeExpectancyAtPoorestIncome +
                    human.DoublingsOfIncome * LifeExpectancyPerDoublingOfIncome;
            }
        }

        #endregion Life Expectancy
    }
}
