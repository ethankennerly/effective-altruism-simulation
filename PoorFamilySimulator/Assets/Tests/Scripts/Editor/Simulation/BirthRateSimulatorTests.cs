using NUnit.Framework;
using PoorFamily.Simulation;
using System;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Tests.Simulation
{
    public sealed class BirthRateSimulatorTests
    {
        [Test]
        public void CalculateEachByIncome_4XPoorestIncome_BirthRateReducedTwice()
        {
            List<Human> humans = new List<Human>{new Human{DoublingsOfIncome = 2}};
            BirthRateSimulator birthRateSim = new BirthRateSimulator(humans);
            birthRateSim.BirthRateAtPoorestIncome = 1f / 16f;
            birthRateSim.BirthRatePerDoublingOfIncome = -1f / 256f;
            birthRateSim.CalculateEachByIncome();
            Assert.AreEqual(1f / 16f - 1f / 128f, humans[0].BirthRate);
        }
    }
}
