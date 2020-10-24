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
            BirthSimulator simulator = new BirthSimulator(humans);
            Human female = humans[0];
            female.Age++;
            simulator.TryBirth();
            Assert.AreEqual(3, humans.Count);
        }

        [Test]
        public void TryBirth_TwoHumansHalfOfBirthPerLifeYear_ThreeHumans()
        {
            AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild();
        }

        [Test]
        public void TryBirth_TwoHumansHalfOfBirthPerLifeYear_ChildIsFemale()
        {
            Human child = AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild();
            Assert.IsTrue(child.IsFemale);
        }

        [Test]
        public void TryBirth_TwoHumansHalfOfBirthPerLifeYear_ChildAgeEquals0()
        {
            Human child = AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild();
            Assert.AreEqual(0f, child.Age);
        }

        [Test]
        public void TryBirth_38LifeYears_4Humans()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator simulator = new BirthSimulator(humans);
            simulator.TryBirth();
            foreach (Human human in humans)
            {
                human.Age += 3.5f;
            }
            simulator.TryBirth();
            Assert.AreEqual(4, humans.Count);
        }

        [Test]
        public void ActualBirthRate_For100Years_Between30And50Per1000()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator simulator = new BirthSimulator(humans);
            float yearIncrement = 1f / 32f;
            for (float years = 0f; years < 100f; years += yearIncrement)
            {
                foreach (Human human in humans)
                {
                    human.Age += yearIncrement;
                }
                simulator.TryBirth();
            }
            Assert.That(simulator.ActualBirthRate, Is.EqualTo(0.04f).Within(0.01f));
        }

        private static Human AssertTwoHumansHalfOfBirthPerLifeYear_GetOneChild()
        {
            List<Human> humans = SetUpHumansOneYearBeforeFemaleIsFertile();
            BirthSimulator simulator = new BirthSimulator(humans);
            Human female = humans[0];
            female.Age++;
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
