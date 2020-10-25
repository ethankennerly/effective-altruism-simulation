using System;
using System.Collections.Generic;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class BirthRateSimulator
    {
        public float BirthRateAtPoorestIncome = 1f / 16f;
        public float BirthRatePerDoublingOfIncome = -1f / 256f;

        private readonly List<Human> m_Humans;

        public BirthRateSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void CalculateEachByIncome()
        {
            foreach (Human human in m_Humans)
            {
                human.BirthRate = BirthRateAtPoorestIncome +
                    human.DoublingsOfIncome * BirthRatePerDoublingOfIncome;
            }
        }
    }
}
