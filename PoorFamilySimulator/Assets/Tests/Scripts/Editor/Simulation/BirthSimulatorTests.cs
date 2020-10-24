using NUnit.Framework;
using PoorFamily.Simulation;
using System;
using System.Collections.Generic;

namespace PoorFamily.Tests.Simulation
{
    public sealed class BirthSimulatorTests
    {
        [Test]
        public void TryBirth_ZeroHumans_0()
        {
            List<Human> humans = new List<Human>();
            BirthSimulator simulator = new BirthSimulator(humans);
            simulator.TryBirth();
            Assert.AreEqual(0, humans.Count);
        }

        [Test]
        public void TryBirth_FemaleNotInFertileAgeRange_TwoHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator simulator = new BirthSimulator(humans);
            simulator.TryBirth();
            Assert.AreEqual(2, humans.Count);
        }

        [Test]
        public void TryBirth_TwoFemales_TwoHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator simulator = new BirthSimulator(humans);
            humans[0].Age += 20f;
            humans[0].IsFemale = true;
            humans[1].Age += 20f;
            humans[1].IsFemale = true;
            simulator.TryBirth();
            Assert.AreEqual(2, humans.Count);
        }

        [Test]
        public void TryBirth_TwoMales_TwoHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator simulator = new BirthSimulator(humans);
            humans[0].Age += 20f;
            humans[0].IsFemale = false;
            humans[1].Age += 20f;
            humans[1].IsFemale = false;
            simulator.TryBirth();
            Assert.AreEqual(2, humans.Count);
        }

        [Test]
        public void TryBirth_FemaleAndMaleInFertileAgeRange_ThreeHumans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            Human female = humans[0];
            BirthSimulator simulator = new BirthSimulator(humans);
            female.Age++;
            simulator.TryBirth();
            Assert.AreEqual(3, humans.Count);
        }

        [Test]
        public void TryBirth_TwoHumansMaxOneChildPerFemalePerYear_ThreeHumans()
        {
            AssertTwoHumansMaxOneChildPerFemalePerYear_GetOneChild();
        }

        [Test]
        public void TryBirth_TwoHumansMaxOneChildPerFemalePerYear_ChildIsFemale()
        {
            Human child = AssertTwoHumansMaxOneChildPerFemalePerYear_GetOneChild();
            Assert.IsTrue(child.IsFemale);
        }

        [Test]
        public void TryBirth_TwoHumansMaxOneChildPerFemalePerYear_ChildAgeEquals0()
        {
            Human child = AssertTwoHumansMaxOneChildPerFemalePerYear_GetOneChild();
            Assert.AreEqual(0f, child.Age);
        }

        private static Human AssertTwoHumansMaxOneChildPerFemalePerYear_GetOneChild()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator simulator = new BirthSimulator(humans);
            humans[0].Age += 20f;
            humans[1].Age += 20f;
            simulator.TryBirth();
            Assert.AreEqual(3, humans.Count, "Number of humans after one new child birth");
            Human child = humans[2];
            return child;
        }

        private static List<Human> SetUpHumansOneYearBeforeFemaleIsFertile()
        {
            Human female = new Human
            {
                Age = 13f,
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
                Age = 13f,
                IsFemale = false,
                BirthRate = 0.05f,
                FertileAgeRange = new FloatRange
                {
                    Min = 13f,
                    Max = 70f
                }
            };
            List<Human> humans = new List<Human>{female, male};
            return humans;
        }
    }
}
