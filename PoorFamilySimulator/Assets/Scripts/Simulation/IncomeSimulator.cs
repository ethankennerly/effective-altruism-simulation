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

        public IncomeSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void AddYears(float deltaYears)
        {
            CalculateRaise(deltaYears);
            TransferBySchedule(deltaYears);

            AverageDoublingsOfIncome = CalculateAverageDoublingsOfIncome();
            NormalizedIncome = Mathf.Clamp01(AverageDoublingsOfIncome / RichestDoublings);
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

        /// <summary>
        /// This overwrites any transfer not applied yet.
        /// </summary>
        public void ScheduleTransfer(List<float> additionInFutureYears)
        {
            m_NextAdditionInFutureYears = additionInFutureYears;
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

            int numHumans = m_Humans.Count;
            if (numHumans == 0)
            {
                return;
            }

            int numTransfers = additionInFutureYears.Count;
            List<float> averageTransfers = new List<float>(numTransfers);
            for (int transferIndex = 0; transferIndex < numTransfers; ++transferIndex)
            {
                float averageTransfer = additionInFutureYears[transferIndex] / numHumans;
                averageTransfers.Add(averageTransfer);
            }

            foreach (Human human in m_Humans)
            {
                if (human.ScheduledTransfers == null)
                {
                    human.ScheduledTransfers = new List<float>(averageTransfers.Count);
                }

                int yearIndex = 0;
                for (; yearIndex < human.ScheduledTransfers.Count; ++yearIndex)
                {
                    human.ScheduledTransfers[yearIndex] += averageTransfers[yearIndex];
                }

                for (; yearIndex < numTransfers; ++yearIndex)
                {
                    human.ScheduledTransfers.Add(averageTransfers[yearIndex]);
                }

                human.Income += human.ScheduledTransfers[0];
            }
        }

        #endregion Scheduling Transfers

        public float CalculateAverageDoublingsOfIncome()
        {
            CalculateNormalizedIncome();

            int numHumans = m_Humans.Count;
            if (numHumans == 0)
            {
                return 0f;
            }

            float sum = 0f;
            foreach (Human human in m_Humans)
            {
                sum += human.DoublingsOfIncome;
            }

            float averageWealth = sum / numHumans;
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
