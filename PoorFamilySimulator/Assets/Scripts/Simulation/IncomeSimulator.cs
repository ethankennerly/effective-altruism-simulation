using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class IncomeSimulator
    {
        private const int kDoublingBase = 2;

        [Range(0, 10)] public float AverageDoublingsOfIncome;
        [Range(0f, 1f)] public float NormalizedIncome;

        [Range(100, 102400)] public float PoorestIncome = 100f;
        [Range(100, 102400)] public float RichestIncome = 102400f;

        public float PoorestDoublings;
        [Range(0, 10)] public float RichestDoublings;

        public float MinAge = 10f;
        public float RaisePerYear = 30f;
        public float PeakAge = 30f;
        public float RaisePerYearAfterPeak = -20f;
        public float BirthRateAtPoorestIncome = 1f / 16f;
        public float BirthRatePerDoublingOfIncome = -1f / 256f;

        private readonly List<Human> m_Humans;

        public IncomeSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void AddYears(float deltaYears)
        {
            CalculateRaise(deltaYears);

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
    }
}
