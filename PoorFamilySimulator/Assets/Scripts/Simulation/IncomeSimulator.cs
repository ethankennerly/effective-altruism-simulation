using System;
using System.Collections.Generic;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class IncomeSimulator
    {
        public float MinAge = 10f;
        public float RaisePerYear = 2f;
        public float PeakAge = 30f;
        public float RaisePerYearAfterPeak = -2f;

        private readonly List<Human> m_Humans;

        public IncomeSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void AddYears(float deltaYears)
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
    }
}