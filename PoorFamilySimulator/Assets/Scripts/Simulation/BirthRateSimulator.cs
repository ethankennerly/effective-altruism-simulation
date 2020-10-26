using System;
using System.Collections.Generic;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class BirthRateSimulator
    {
        public float BirthRateAtPoorestIncome = 64f / 1024f;
        public float BirthRatePerDoublingOfIncome = -4f / 1024f;
        public float BirthRateForLiterateFemale = -20f / 1024f;
        public float AverageBirthRate;

        private readonly List<Human> m_Humans;

        public BirthRateSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void CalculateEach()
        {
            if (m_Humans.Count == 0)
            {
                AverageBirthRate = 0f;
            }

            float sumOfBirthRates = 0f;
            foreach (Human human in m_Humans)
            {
                human.BirthRate = BirthRateAtPoorestIncome +
                    human.DoublingsOfIncome * BirthRatePerDoublingOfIncome;
                sumOfBirthRates += human.BirthRate;
            }
            AverageBirthRate = sumOfBirthRates / m_Humans.Count;
        }
    }
}
