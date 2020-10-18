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
        public void AverageDoublingsOfIncome_200And400ThenAddTime_Equals5()
        {
            Simulator simulator = new Simulator{PoorestIncome = 100};
            Human income200 = new Human{Income = 6400};
            Human income400 = new Human{Income = 1600};
            simulator.Humans.Add(income200);
            simulator.Humans.Add(income400);
            simulator.AddTime(0.001f);
            Assert.AreEqual(5f, simulator.AverageDoublingsOfIncome);
        }

        [Test]
        public void NormalizedWealth_ZeroHumans_Zero()
        {
            Simulator simulator = new Simulator();
            simulator.AddTime(0.001f);
            Assert.AreEqual(0f, simulator.NormalizedWealth);
        }

        [Test]
        public void NormalizedWealth_200And400ThenAddTime_EqualsHalf()
        {
            Simulator simulator = new Simulator{PoorestIncome = 100};
            Human income200 = new Human{Income = 1600};
            Human income400 = new Human{Income = 6400};
            simulator.Humans.Add(income200);
            simulator.Humans.Add(income400);
            simulator.AddTime(0.001f);
            Assert.AreEqual(0.5f, simulator.NormalizedWealth);
        }
    }
}
