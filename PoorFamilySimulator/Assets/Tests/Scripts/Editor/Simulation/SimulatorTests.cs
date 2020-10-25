using NUnit.Framework;
using PoorFamily.Simulation;
using System;

namespace PoorFamily.Tests.Simulation
{
    public sealed class SimulatorTests
    {
        [Test]
        public void AddTime_HumansUpdated_SetNormalizedIncome()
        {
            float normalizedIncome = 0f;
            Action<Simulator> setNormalizedIncome = (updatedSimulator) =>
                normalizedIncome = updatedSimulator.Income.NormalizedIncome;

            Simulator simulator = new Simulator();
            simulator.Income.PoorestIncome = 100;
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
