using System;
using System.Collections.Generic;
using System.Text;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class BirthSimulator
    {
        public float HistoricalBirthRate;
        public int NumHumans;
        private float m_ExpectedBirthRate;
        private readonly List<float> m_BirthRateInFullYears = new List<float>();
        private int m_FullYears;
        private float m_PartialYears;
        private float m_NumBirthsInPartialYears;
        private float m_SumOfBirthRateInFullYears;
        private bool m_NextChildIsFemale = true;
        private bool m_TriedBirthThisYear;
        private float m_MaxHumans = 10000;

        private readonly List<Human> m_Humans;

        public BirthSimulator(List<Human> humans)
        {
            m_Humans = humans;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Num Humans: ").Append(NumHumans);
            return sb.ToString();
        }

        public string HistoricalRatesToString()
        {
            return string.Join(", ", m_BirthRateInFullYears);
        }

        /// <summary>
        /// Recalculates number of humans in case that changed externally.
        /// </summary>
        public void AddYears(float deltaYears)
        {
            NumHumans = m_Humans.Count;
            HistoricalBirthRate = CalculateHistoricalBirthRate(deltaYears);
            m_ExpectedBirthRate = CalculateExpectedBirthRate(m_Humans);
            AddAgeToHumans(deltaYears);

            if (!m_TriedBirthThisYear)
            {
                m_TriedBirthThisYear = true;
                TryBirth();
            }
        }

        private void AddAgeToHumans(float deltaYears)
        {
            foreach (Human human in m_Humans)
            {
                human.Age += deltaYears;
            }
        }

        /// <summary>
        /// While the expected birth rate is above the historical birth rate,
        /// a fertile female and male give birth to a child.
        /// </summary>
        private void TryBirth()
        {
            if (NumHumans == 0 || NumHumans >= m_MaxHumans)
            {
                return;
            }

            bool anyFertileFemale = false;
            bool anyFertileMale = false;
            foreach (Human human in m_Humans)
            {
                if (human.FertileAgeRange == null ||
                    human.Age < human.FertileAgeRange.Min ||
                    human.Age > human.FertileAgeRange.Max)
                {
                    continue;
                }

                if (human.IsFemale)
                {
                    anyFertileFemale = true;
                }
                else
                {
                    anyFertileMale = true;
                }

                if (anyFertileFemale && anyFertileMale)
                {
                    break;
                }
            }

            if (!anyFertileFemale || !anyFertileMale)
            {
                return;
            }

            float birthRateDifference = m_ExpectedBirthRate - HistoricalBirthRate;
            if (birthRateDifference < 0f)
            {
                return;
            }

            float differenceThisYear = birthRateDifference * (m_FullYears + 1);
            int numBirths = (int)(differenceThisYear * NumHumans + 0.5f);
            for ( ; numBirths > 0; --numBirths)
            {
                Human child = new Human
                {
                    LifeExpectancy = 55,
                    IsFemale = m_NextChildIsFemale,
                    BirthRate = m_ExpectedBirthRate,
                    FertileAgeRange = new FloatRange
                    {
                        Min = 14f,
                        Max = m_NextChildIsFemale ? 42f : 70f,
                    }
                };
                m_Humans.Add(child);
                NumHumans++;
                m_NumBirthsInPartialYears++;
                m_NextChildIsFemale = !m_NextChildIsFemale;
                if (NumHumans >= m_MaxHumans)
                {
                    break;
                }
            }
        }

        private float CalculateExpectedBirthRate(List<Human> humans)
        {
            if (NumHumans <= 0)
            {
                return 0f;
            }

            float sumOfBirthRates = 0f;
            foreach (Human human in m_Humans)
            {
                sumOfBirthRates += human.BirthRate;
            }

            float averageOfBirthRates = sumOfBirthRates / NumHumans;
            return averageOfBirthRates;
        }

        private float CalculateHistoricalBirthRate(float deltaYears)
        {
            for (m_PartialYears += deltaYears; m_PartialYears >= 1f; --m_PartialYears)
            {
                float birthRate = m_NumBirthsInPartialYears / NumHumans;
                m_NumBirthsInPartialYears = 0f;
                m_SumOfBirthRateInFullYears += birthRate;
                m_BirthRateInFullYears.Add(birthRate);
                m_FullYears++;
                m_TriedBirthThisYear = false;
            }

            if (m_FullYears <= 0f)
            {
                return 0f;
            }

            float actualBirthRate = m_SumOfBirthRateInFullYears / m_FullYears;
            return actualBirthRate;
        }
    }
}
