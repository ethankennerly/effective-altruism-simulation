using System;
using System.Collections.Generic;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class LiteracyTeacher
    {
        public float TuitionPerIncome = 20f / 128f;
        public float LiteracyRate;
        public float Funds;

        private readonly List<Human> m_Humans;

        public LiteracyTeacher(List<Human> humans)
        {
            m_Humans = humans;
        }

        public void AddFunds(float funds)
        {
            Funds += funds;
        }

        public void TryTeachEach()
        {
            int numHumans = m_Humans.Count;
            if (numHumans == 0)
            {
                LiteracyRate = 0f;
            }

            float sumOfIsLiterates = 0f;
            foreach (Human human in m_Humans)
            {
                if (!human.IsLiterate)
                {
                    float tuition = human.Income * TuitionPerIncome;
                    if (Funds < tuition)
                    {
                        continue;
                    }

                    Funds -= tuition;
                    human.IsLiterate = true;
                }

                sumOfIsLiterates++;
            }

            LiteracyRate = sumOfIsLiterates / numHumans;
        }
    }
}
