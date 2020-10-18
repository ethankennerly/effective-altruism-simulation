using System.Collections.Generic;

namespace PoorFamily.Simulation
{
    public sealed class Simulator
    {
        public float PoorestIncome = 100f;

        public YearTimer YearTimer { get; } = new YearTimer(2013, 100); 

        public List<Human> Humans { get; } = new List<Human>();

        public void AddTime(float deltaTime)
        {
            YearTimer.AddYears(deltaTime);
        }

        public float CalculateAverageDoublingsOfIncome()
        {
            return 0f;
        }
    }
}
