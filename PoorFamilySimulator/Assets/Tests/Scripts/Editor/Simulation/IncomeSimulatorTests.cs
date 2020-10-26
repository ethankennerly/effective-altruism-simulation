using NUnit.Framework;
using PoorFamily.Simulation;
using System;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

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

        #region Income Scheduler

        [Test]
        public void AddYears_BuildGiveDirectly1205Schedule_Log()
        {
            List<Human> humans = new List<Human>{new Human()};
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            string scheduleString = string.Join(", ", incomeSim.BuildGiveDirectlySchedule(1205f));
            Debug.Log(scheduleString);
        }

        [Test]
        public void AddYears_Raise2AndScheduleTransfer130Then8Then8_Income132Then12Then14Then8()
        {
            List<Human> humans = new List<Human>{new Human()};
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.MinAge = 10f;
            incomeSim.RaisePerYear = 2f;
            Human.AddAgeToEach(humans, 10f);
            incomeSim.AddYears(10f);
            incomeSim.ScheduleTransfer(new List<float>{130f, 8f, 8f});
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(132f, humans[0].Income,
                "Year 1\nScheduled Transfers: " + string.Join(", ", humans[0].ScheduledTransfers));
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(12f, humans[0].Income,
                "Year 2\nScheduled Transfers: " + string.Join(", ", humans[0].ScheduledTransfers));
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(14f, humans[0].Income, "Year 3");
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(8f, humans[0].Income,
                "Year 4, after transfers, income returns to raise amount.");
        }

        [Test]
        public void AddYears_Raise2AndScheduleThreeTransfers130Then8Then8_Income392Then28Then30Then8()
        {
            List<Human> humans = new List<Human>{new Human()};
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.MinAge = 10f;
            incomeSim.RaisePerYear = 2f;
            Human.AddAgeToEach(humans, 10f);
            incomeSim.AddYears(10f);
            incomeSim.ScheduleTransfer(new List<float>{130f, 8f, 8f});
            incomeSim.ScheduleTransfer(new List<float>{130f, 8f, 8f});
            incomeSim.ScheduleTransfer(new List<float>{130f, 8f, 8f});
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(392f, humans[0].Income,
                "Year 1\nScheduled Transfers: " + string.Join(", ", humans[0].ScheduledTransfers));
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(28f, humans[0].Income,
                "Year 2\nScheduled Transfers: " + string.Join(", ", humans[0].ScheduledTransfers));
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(30f, humans[0].Income, "Year 3");
            Human.AddAgeToEach(humans, 1f);
            incomeSim.AddYears(1f);
            Assert.AreEqual(8f, humans[0].Income,
                "Year 4, after transfers, income returns to raise amount.");
        }

        #endregion Income Scheduler

        #region Share Income

        [Test]
        public void AddYears_TwoHumansShareIncome_EqualIncome()
        {
            List<Human> humans = new List<Human>
            {
                new Human{Income = 6400},
                new Human{Income = 1600}
            };
            IncomeSimulator incomeSim = new IncomeSimulator(humans);
            incomeSim.SharingEnabled = true;
            incomeSim.AddYears(1f);
            Assert.AreEqual(4000f, humans[0].Income, "Was 6400");
            Assert.AreEqual(4000f, humans[1].Income, "Was 1600");
        }

        #endregion Share Income

        #region Normalized Income

        [Test]
        public void CalculateAverageDoublingsOfIncome_ZeroHumans_Zero()
        {
            IncomeSimulator incomeSim = new IncomeSimulator(new List<Human>());
            incomeSim.AddYears(0.001f);
            Assert.AreEqual(0f, incomeSim.AverageDoublingsOfIncome);
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
            Assert.AreEqual("0%", incomeSim.NormalizedIncomeString);
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
            Assert.AreEqual("50%", incomeSim.NormalizedIncomeString);
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
