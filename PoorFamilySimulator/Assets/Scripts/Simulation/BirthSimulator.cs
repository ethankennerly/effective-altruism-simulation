// using System;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Simulation
{
    // Inspectable [Serializable]
    public sealed class BirthSimulator
    {
        public float ActualBirthRate;

        private readonly List<Human> m_Humans;

        public BirthSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void TryBirth()
        {
            Debug.LogWarning("TODO: Try Birth");
        }
    }
}
