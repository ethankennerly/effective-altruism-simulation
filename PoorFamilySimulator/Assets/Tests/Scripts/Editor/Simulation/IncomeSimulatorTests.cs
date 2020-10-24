using NUnit.Framework;
using PoorFamily.Simulation;
using System;
using System.Collections.Generic;

namespace PoorFamily.Tests.Simulation
{
    public sealed class IncomeSimulatorTests
    {
        [Test]
        public void AddYears_ZeroHumans_NoCrash()
        {
            IncomeSimulator incomeSim = new IncomeSimulator(new List<Human>());
            incomeSim.AddYears(1f);
        }

        [Test]
        public void AddYears_Age13DeltaIncome2_Income6()
        {
            List<Human> humans = new List<Human>{new Human()};
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.MinAge = 10f;
            incomeSim.DeltaIncome = 2f;
            incomeSim.AddYears(13f);
            Assert.AreEqual(6f, humans[0].Income);
        }
    }
}
