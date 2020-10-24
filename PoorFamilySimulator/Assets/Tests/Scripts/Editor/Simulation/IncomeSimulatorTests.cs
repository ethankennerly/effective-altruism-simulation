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
        public void AddYears_Age13RaisePerYear2_Income6()
        {
            List<Human> humans = new List<Human>{new Human()};
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.MinAge = 10f;
            incomeSim.RaisePerYear = 2f;
            Human.AddAgeToEach(humans, 10f);
            incomeSim.AddYears(10f);
            Human.AddAgeToEach(humans, 3f);
            incomeSim.AddYears(3f);
            Assert.AreEqual(6f, humans[0].Income);
        }

        [Test]
        public void AddYears_Age40RaisePerYearNegative2_Income20()
        {
            List<Human> humans = new List<Human>{new Human()};
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.MinAge = 10f;
            incomeSim.RaisePerYear = 2f;
            incomeSim.PeakAge = 30f;
            incomeSim.RaisePerYearAfterPeak = -2f;
            Human.AddAgeToEach(humans, 10f);
            incomeSim.AddYears(10f);
            Human.AddAgeToEach(humans, 10f);
            incomeSim.AddYears(10f);
            Human.AddAgeToEach(humans, 10f);
            incomeSim.AddYears(10f);
            Human.AddAgeToEach(humans, 10f);
            incomeSim.AddYears(10f);
            Assert.AreEqual(20f, humans[0].Income);
        }
    }
}
