using System.Collections.Generic;

namespace PoorFamily.Simulation
{
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
    }
}
