using FineGameDesign.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class Simulator
    {
        private const int kDoublingBase = 2;

        [Range(0, 10)] public float AverageDoublingsOfIncome;
        [Range(0f, 1f)] public float NormalizedIncome;

        [Range(100, 102400)] public float PoorestIncome = 100f;
        [Range(100, 102400)] public float RichestIncome = 102400f;
        public float PoorestDoublings;
        [Range(0, 10)] public float RichestDoublings;

        public readonly ActionEvent<Simulator> Updated = new ActionEvent<Simulator>();

        public YearTimer YearTimer { get; } = new YearTimer(2013, 100); 

        public List<Human> Humans = new List<Human>();

        public BirthSimulator BirthSimulator;
        private DeathSimulator m_DeathSimulator;
        public IncomeSimulator IncomeSimulator;

        public Simulator()
        {
            BirthSimulator = new BirthSimulator(Humans);
            IncomeSimulator = new IncomeSimulator(Humans);
            m_DeathSimulator = new DeathSimulator(Humans);
            Updated.SetValue(this);
        }

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime);
            float deltaYears = YearTimer.DeltaYears;
            BirthSimulator.AddYears(deltaYears);
            IncomeSimulator.AddYears(deltaYears);
            m_DeathSimulator.TryDeath();

            AverageDoublingsOfIncome = CalculateAverageDoublingsOfIncome();
            NormalizedIncome = Mathf.Clamp01(AverageDoublingsOfIncome / RichestDoublings);

            Updated.TryInvoke(this);
        }

        public float CalculateAverageDoublingsOfIncome()
        {
            PoorestDoublings = Mathf.Log(PoorestIncome, kDoublingBase);
            RichestDoublings = Mathf.Log(RichestIncome, kDoublingBase) - PoorestDoublings;

            int numHumans = Humans.Count;
            if (numHumans == 0)
            {
                return 0f;
            }

            float sum = 0f;
            foreach (Human human in Humans)
            {
                human.DoublingsOfIncome = human.Income <= 1f ?
                    0f :
                    Mathf.Log(human.Income, kDoublingBase) - PoorestDoublings;
                human.NormalizedIncome = Mathf.Clamp01(human.DoublingsOfIncome / RichestDoublings);
                sum += human.DoublingsOfIncome;
            }

            float averageWealth = sum / numHumans;
            return averageWealth;
        }
    }
}
