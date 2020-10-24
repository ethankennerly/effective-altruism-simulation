using NUnit.Framework;
using PoorFamily.Simulation;
using System;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Tests.Simulation
{
    public sealed class DeathSimulatorTests
    {
        [Test]
        public void AddYears_ZeroHumans_0()
        {
            List<Human> humans = new List<Human>();
            DeathSimulator simulator = new DeathSimulator(humans);
            simulator.AddYears(0f);
            Assert.AreEqual(0, humans.Count);
        }

        [Test]
        public void AddYears53_LifeExpectancy54And56_TwoHumans()
        {
            List<Human> humans = SetUpHumansLifeExpectancy54And56();
            DeathSimulator simulator = new DeathSimulator(humans);
            simulator.AddYears(53f);
            Assert.AreEqual(2, humans.Count);
        }

        [Test]
        public void AddYears54_LifeExpectancy54And56_OneHumans()
        {
            List<Human> humans = SetUpHumansLifeExpectancy54And56();
            DeathSimulator simulator = new DeathSimulator(humans);
            simulator.AddYears(54f);
            Assert.AreEqual(1, humans.Count);
        }

        [Test]
        public void AddYears56_LifeExpectancy54And56_ZeroHumans()
        {
            List<Human> humans = SetUpHumansLifeExpectancy54And56();
            DeathSimulator simulator = new DeathSimulator(humans);
            simulator.AddYears(56f);
            Assert.AreEqual(0, humans.Count);
        }

        private static List<Human> SetUpHumansLifeExpectancy54And56()
        {
            Human willLiveTo54 = new Human{LifeExpectancy = 54};
            Human willLiveTo56 = new Human{LifeExpectancy = 56};
            List<Human> humans = new List<Human>{willLiveTo54, willLiveTo56};
            return humans;
        }
    }
}
