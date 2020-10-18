using NUnit.Framework;
using PoorFamily.Simulation;
using System;

namespace PoorFamily.Simulation.Tests
{
    public sealed class SimulatorTests
    {
        [Test]
        public void CalculateAverageDoublingsOfIncome_ZeroHumans_Zero()
        {
            Simulator simulator = new Simulator();
            Assert.AreEqual(0f, simulator.CalculateAverageDoublingsOfIncome());
        }

        [Test]
        public void CalculateAverageDoublingsOfIncome_200And400_Equals1Point5()
        {
            Simulator simulator = new Simulator{PoorestIncome = 100};
            Human income200 = new Human{Income = 200};
            Human income400 = new Human{Income = 400};
            simulator.Humans.Add(income200);
            simulator.Humans.Add(income400);
            Assert.AreEqual(1.5f, simulator.CalculateAverageDoublingsOfIncome());
        }
    }
}
