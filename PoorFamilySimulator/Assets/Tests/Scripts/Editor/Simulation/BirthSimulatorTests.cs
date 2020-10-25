using NUnit.Framework;
using PoorFamily.Simulation;
using System;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Tests.Simulation
{
    public sealed class BirthSimulatorTests
    {
        [Test]
        public void AddYears_ZeroHumans_0()
        {
            List<Human> humans = new List<Human>();
            BirthSimulator birthSim = new BirthSimulator(humans);
            birthSim.AddYears(0f);
            Assert.AreEqual(0, humans.Count);
        }

        [Test]
        public void AddYears_FemaleNotInFertileAgeRange_TwoHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            birthSim.AddYears(13f);
            Assert.AreEqual(2, humans.Count);
        }

        [Test]
        public void AddYears_TwoFemales_TwoHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            humans[0].IsFemale = true;
            humans[1].IsFemale = true;
            birthSim.AddYears(20f);
            Assert.AreEqual(2, humans.Count);
        }

        [Test]
        public void AddYears_TwoMales_TwoHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            humans[0].IsFemale = false;
            humans[1].IsFemale = false;
            birthSim.AddYears(20f);
            Assert.AreEqual(2, humans.Count);
        }

        [Test]
        public void AddYears_FemaleAndMaleInFertileAgeRange_ThreeHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            Human female = humans[0];
            birthSim.AddYears(14f);
            Assert.AreEqual(3, humans.Count);
        }

        [Test]
        public void AddYears_TwoHumansHalfOfBirthPerLifeYear_ThreeHumans()
        {
            AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild();
        }

        [Test]
        public void AddYears_TwoHumansHalfOfBirthPerLifeYear_ChildIsFemale()
        {
            Human child = AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild();
            Assert.IsTrue(child.IsFemale);
        }

        [Test]
        public void AddYears_TwoHumansHalfOfBirthPerLifeYear_ChildHasLifeExpectancyOf55()
        {
            Human child = AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild();
            Assert.AreEqual(55, child.LifeExpectancy);
        }

        [Test]
        public void AddYears_TwoHumansHalfOfBirthPerLifeYear_ChildAgeEquals0()
        {
            Human child = AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild();
            Assert.AreEqual(0f, child.Age);
        }

        [Test]
        public void AddYears_25Years_4Humans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            birthSim.AddYears(25f);
            Assert.AreEqual(4, humans.Count);
        }

        [Test]
        public void AddYears_25Years_SecondChildIsNotFemale()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            birthSim.AddYears(25f);
            Assert.AreEqual(4, humans.Count);
            Assert.IsFalse(humans[3].IsFemale);
        }

        [Test]
        public void HistoricalBirthRate_For100Years_Between30And50Per1000()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            float deltaYears = 1f / 64f;
            for (float years = 0f; years < 100f; years += deltaYears)
            {
                birthSim.AddYears(deltaYears);
            }
            Assert.That(
                birthSim.HistoricalBirthRate,
                Is.EqualTo(0.04f).Within(0.01f),
                birthSim.ToString()
            );

            Debug.Log(birthSim.ToString());
            Debug.Log(birthSim.HistoricalRatesToString());
        }

        private static Human AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator birthSim = new BirthSimulator(humans);
            birthSim.AddYears(14f);
            Assert.AreEqual(3, humans.Count, "Number of humans after one new child birth");
            Human child = humans[2];
            return child;
        }

        private static List<Human> SetUpHumansOneYearBeforeFemaleIsFertile()
        {
            Human female = new Human
            {
                Age = 0f,
                IsFemale = true,
                BirthRate = 0.03f,
                FertileAgeRange = new FloatRange
                {
                    Min = 14f,
                    Max = 42f
                }
            };
            Human male = new Human
            {
                Age = 1f,
                IsFemale = false,
                BirthRate = 0.05f,
                FertileAgeRange = new FloatRange
                {
                    Min = 14f,
                    Max = 70f
                }
            };
            List<Human> humans = new List<Human>{female, male};
            return humans;
        }
    }
}
