using System.Collections.Generic;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Simulation
{
    public sealed class BirthSimulator
    {
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
