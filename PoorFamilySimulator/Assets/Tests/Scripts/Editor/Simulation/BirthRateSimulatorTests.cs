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
        public void CalculateEach_4XPoorestIncome_BirthRateReducedTwice()
        {
            List<Human> humans = new List<Human>{new Human{DoublingsOfIncome = 2}};
            BirthRateSimulator birthRateSim = new BirthRateSimulator(humans);
            birthRateSim.BirthRateAtPoorestIncome = 1f / 16f;
            birthRateSim.BirthRatePerDoublingOfIncome = -1f / 256f;
            birthRateSim.CalculateEach();
            Assert.AreEqual(1f / 16f - 1f / 128f, humans[0].BirthRate);
        }

        [Test]
        public void CalculateEach_4XPoorestIncomeAndFemaleLiterate_BirthRateReducedTwiceAndByLiteracy()
        {
            BirthRateSimulator birthRateSim = SetUp4XPoorestIncomeByIsFemale(true);
            birthRateSim.CalculateEach();
            Assert.AreEqual((64f - 8f - 20f) / 1024f, birthRateSim.AverageBirthRate);
        }

        [Test]
        public void CalculateEach_4XPoorestIncomeAndMaleLiterate_BirthRateReducedTwiceNotByLiteracy()
        {
            BirthRateSimulator birthRateSim = SetUp4XPoorestIncomeByIsFemale(false);
            birthRateSim.CalculateEach();
            Assert.AreEqual((64f - 8f) / 1024f, birthRateSim.AverageBirthRate);
        }

        private BirthRateSimulator SetUp4XPoorestIncomeByIsFemale(bool isFemale)
        {
            List<Human> humans = new List<Human>{
                new Human{DoublingsOfIncome = 2, IsFemale = isFemale, IsLiterate = true}
            };
            BirthRateSimulator birthRateSim = new BirthRateSimulator(humans);
            birthRateSim.BirthRateAtPoorestIncome = 64f / 1024f;
            birthRateSim.BirthRatePerDoublingOfIncome = -4f / 1024f;
            birthRateSim.BirthRateForLiterateFemale = -20f / 1024f;
            return birthRateSim;
        }
    }
}
