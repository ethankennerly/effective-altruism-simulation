using NUnit.Framework;
using PoorFamily.Simulation;
using System;
using System.Collections.Generic;

namespace PoorFamily.Tests.Simulation
{
    public sealed class IncomeSimulatorTests
    {
        #region Raise

        [Test]
        public void AddYears_ZeroHumans_NoError()
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

        #endregion Raise

        #region Normalized Income

        [Test]
        public void CalculateAverageDoublingsOfIncome_ZeroHumans_Zero()
        {
            IncomeSimulator incomeSim = new IncomeSimulator(new List<Human>());
            Assert.AreEqual(0f, incomeSim.CalculateAverageDoublingsOfIncome());
        }

        [Test]
        public void AverageDoublingsOfIncome_0AddYears_ClampsTo0()
        {
            IncomeSimulator incomeSim = new IncomeSimulator(new List<Human>{new Human()});
            incomeSim.AddYears(0.001f);
            Assert.AreEqual(0f, incomeSim.AverageDoublingsOfIncome);
        }

        [Test]
        public void AverageDoublingsOfIncome_6400And1600ThenAddYears_Equals5()
        {
            List<Human> humans = new List<Human>
            {
                new Human{Income = 6400},
                new Human{Income = 1600}
            };
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.PoorestIncome = 100;
            incomeSim.AddYears(0.001f);
            Assert.AreEqual(5f, incomeSim.AverageDoublingsOfIncome);
        }

        [Test]
        public void NormalizedIncome_ZeroHumans_Zero()
        {
            IncomeSimulator incomeSim = new IncomeSimulator(new List<Human>());
            incomeSim.AddYears(0.001f);
            Assert.AreEqual(0f, incomeSim.NormalizedIncome);
        }

        [Test]
        public void NormalizedIncome_6400And1600ThenAddYears_EqualsHalf()
        {
            List<Human> humans = new List<Human>
            {
                new Human{Income = 6400},
                new Human{Income = 1600}
            };
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.PoorestIncome = 100;
            incomeSim.AddYears(0.001f);
            Assert.AreEqual(0.5f, incomeSim.NormalizedIncome);
        }

        #endregion Normalized Income

        #region Life Expectancy

        [Test]
        public void CalculateLifeExpectancy_1XPoorestIncome_LifeExpectancy47()
        {
            AssertLifeExpectancyByDoublingsOfIncome(0f);
        }

        [Test]
        public void CalculateLifeExpectancy_4XPoorestIncome_LifeExpectancy55()
        {
            AssertLifeExpectancyByDoublingsOfIncome(2f);
        }

        [Test]
        public void CalculateLifeExpectancy_1024XPoorestIncome_LifeExpectancy87()
        {
            AssertLifeExpectancyByDoublingsOfIncome(10f);
        }

        private static void AssertLifeExpectancyByDoublingsOfIncome(float doublingsOfIncome)
        {
            List<Human> humans = new List<Human>{new Human{DoublingsOfIncome = doublingsOfIncome}};
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.LifeExpectancyAtPoorestIncome = 47f;
            incomeSim.LifeExpectancyPerDoublingOfIncome = 4f;
            incomeSim.CalculateLifeExpectancy();
            Assert.AreEqual(47f + doublingsOfIncome * 4f, humans[0].LifeExpectancy);
        }

        #endregion Life Expectancy
    }
}
