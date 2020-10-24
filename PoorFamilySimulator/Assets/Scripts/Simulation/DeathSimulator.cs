using System;
using System.Collections.Generic;
using System.Text;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class DeathSimulator
    {
        private readonly List<Human> m_Humans;

        public DeathSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void TryDeath()
        {
            for (int humanIndex = m_Humans.Count - 1; humanIndex >= 0; --humanIndex)
            {
                Human human = m_Humans[humanIndex];
                if (human.Age < human.LifeExpectancy)
                {
                    continue;
                }

                m_Humans.RemoveAt(humanIndex);
            }
        }

        public void AddYears(float deltaYears)
        {
            AddAgeToHumans(deltaYears);
            TryDeath();
        }

        private void AddAgeToHumans(float deltaYears)
        {
            foreach (Human human in m_Humans)
            {
                human.Age += deltaYears;
            }
        }
    }
}
