// using System;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Simulation
{
    // Inspectable [Serializable]
    public sealed class BirthSimulator
    {
        public float ActualBirthRate;
        private float m_ExpectedBirthRate;
        private List<float> m_BirthRateHistory = new List<float>();
        private float m_PartialYears;

        private readonly List<Human> m_Humans;

        public BirthSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void AddYears(float deltaYears)
        {
            TryBirth();
        }

        private void TryBirth()
        {
            Debug.LogWarning("TODO: Try Birth");
        }
    }
}
