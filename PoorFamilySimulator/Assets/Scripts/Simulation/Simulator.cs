using System.Collections.Generic;

using Mathf = UnityEngine.Mathf;

namespace PoorFamily.Simulation
{
    public sealed class Simulator
    {
        private const int kDoublingBase = 2;

        public float PoorestIncome = 100f;

        public YearTimer YearTimer { get; } = new YearTimer(2013, 100); 

        public List<Human> Humans { get; } = new List<Human>();

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime);
        }

        public float CalculateAverageDoublingsOfIncome()
        {
            int numHumans = Humans.Count;
            if (numHumans == 0)
            {
                return 0f;
            }

            float baseline = Mathf.Log(PoorestIncome, kDoublingBase);
            float sum = 0f;
            foreach (Human human in Humans)
            {
                human.DoublingsOfIncome = Mathf.Log(human.Income, kDoublingBase) - baseline;
                sum += human.DoublingsOfIncome;
            }

            return sum / numHumans;
        }
    }
}
