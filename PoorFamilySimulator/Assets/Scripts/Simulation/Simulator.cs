using System;
using System.Collections.Generic;

using Mathf = UnityEngine.Mathf;
using UnityEngine;

namespace PoorFamily.Simulation
{
    [Serializable]
    public sealed class Simulator
    {
        private const int kDoublingBase = 2;

        [Range(0, 10)] public float AverageDoublingsOfIncome;
        [Range(0f, 1f)] public float NormalizedWealth;

        [Range(100, 102400)] public float PoorestIncome = 100f;
        [Range(100, 102400)] public float RichestIncome = 102400f;
        public float PoorestDoublings;
        [Range(0, 10)] public float RichestDoublings;

        public YearTimer YearTimer { get; } = new YearTimer(2013, 100); 

        public List<Human> Humans = new List<Human>();

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime);
            AverageDoublingsOfIncome = CalculateAverageDoublingsOfIncome();

            PoorestDoublings = Mathf.Log(PoorestIncome, kDoublingBase);
            RichestDoublings = Mathf.Log(RichestIncome, kDoublingBase) - PoorestDoublings;
            NormalizedWealth = Mathf.Clamp01(AverageDoublingsOfIncome / RichestDoublings);
        }

        public float CalculateAverageDoublingsOfIncome()
        {
            int numHumans = Humans.Count;
            if (numHumans == 0)
            {
                return 0f;
            }

            float PoorestDoublings = Mathf.Log(PoorestIncome, kDoublingBase);
            float sum = 0f;
            foreach (Human human in Humans)
            {
                human.DoublingsOfIncome = Mathf.Log(human.Income, kDoublingBase) - PoorestDoublings;
                sum += human.DoublingsOfIncome;
            }

            return sum / numHumans;
        }
    }
}
