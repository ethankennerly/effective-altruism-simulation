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
        public void AverageDoublingsOfIncome_6400And1600ThenAddTime_Equals5()
        {
            Simulator simulator = new Simulator{PoorestIncome = 100};
            Human income6400 = new Human{Income = 6400};
            Human income1600 = new Human{Income = 1600};
            simulator.Humans.Add(income6400);
            simulator.Humans.Add(income1600);
            simulator.AddTime(0.001f);
            Assert.AreEqual(5f, simulator.AverageDoublingsOfIncome);
        }

        [Test]
        public void NormalizedIncome_ZeroHumans_Zero()
        {
            Simulator simulator = new Simulator();
            simulator.AddTime(0.001f);
            Assert.AreEqual(0f, simulator.NormalizedIncome);
        }

        [Test]
        public void NormalizedIncome_6400And1600ThenAddTime_EqualsHalf()
        {
            Simulator simulator = new Simulator{PoorestIncome = 100};
            Human income6400 = new Human{Income = 6400};
            Human income1600 = new Human{Income = 1600};
            simulator.Humans.Add(income6400);
            simulator.Humans.Add(income1600);
            simulator.AddTime(0.001f);
            Assert.AreEqual(0.5f, simulator.NormalizedIncome);
        }

        [Test]
        public void AddTime_HumansUpdated_SetNormalizedIncome()
        {
            float normalizedIncome = 0f;
            Action<Simulator> setNormalizedIncome = (updatedSimulator) =>
                normalizedIncome = updatedSimulator.NormalizedIncome;

            Simulator simulator = new Simulator{PoorestIncome = 100};
            Human income102400 = new Human{Income = 102400};
            simulator.Humans.Add(income102400);
            simulator.AddTime(0.001f);
            simulator.Updated.OnInvoke += setNormalizedIncome;
            Assert.AreEqual(1f, normalizedIncome, "Richest human added");

            Human income100 = new Human{Income = 100};
            simulator.Humans.Add(income100);
            simulator.AddTime(0.001f);
            Assert.AreEqual(0.5f, normalizedIncome, "Poorest human added");

            simulator.Updated.OnInvoke -= setNormalizedIncome;
            simulator.Humans.Remove(income100);
            simulator.AddTime(0.001f);
            Assert.AreEqual(0.5f, normalizedIncome, "Removed listener");
        }
    }
}
