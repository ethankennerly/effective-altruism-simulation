using System;
using System.Collections.Generic;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class IncomeSimulator
    {
        public float MinAge = 10f;
        public float RaisePerYear = 2f;

        private readonly List<Human> m_Humans;

        public IncomeSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void AddYears(float deltaYears)
        {
            float deltaIncome = RaisePerYear * deltaYears;
            foreach (Human human in m_Humans)
            {
                if (human.Age <= MinAge)
                {
                    continue;
                }

                human.Income += deltaIncome;
            }
        }
    }
}
