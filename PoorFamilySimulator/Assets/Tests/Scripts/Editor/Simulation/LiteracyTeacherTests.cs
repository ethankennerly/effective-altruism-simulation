using NUnit.Framework;
using PoorFamily.Simulation;
using PoorFamily.Simulation.Donation;
using System;
using System.Collections.Generic;

namespace PoorFamily.Tests.Simulation
{
    public sealed class LiteracyTeacherTests
    {
        [Test]
        public void TryTeachEach_UnderFunded_Illiterate()
        {
            List<Human> humans = new List<Human>{new Human{Income = 128f}};
            LiteracyTeacher teacher = new LiteracyTeacher(humans);
            teacher.TuitionPerIncome = 20f / 128f;
            teacher.MinTuition = 10f;
            teacher.TryTeachEach();
            Assert.AreEqual(0f, teacher.LiteracyRate);
        }

        [Test]
        public void TryTeachEach_FundedTwice_TwoOfThreeLiterate()
        {
            List<Human> humans = new List<Human>
            {
                new Human{Income = 128f},
                new Human{Income = 128f},
                new Human{Income = 128f},
                new Human{Income = 128f}
            };
            LiteracyTeacher teacher = new LiteracyTeacher(humans);
            teacher.TuitionPerIncome = 20f / 128f;
            teacher.MinTuition = 10f;
            teacher.AddFunds(79f);
            teacher.TryTeachEach();
            Assert.AreEqual(0.75f, teacher.LiteracyRate);
        }

        [Test]
        public void DonorOptionPrathamFunded_TryTeachEach_LiteracyRate1AndNoRoomForFunding()
        {
            List<Human> humans = new List<Human>{new Human{Income = 128f}};
            Donor donor = new Donor();
            donor.OptionMenu.Pratham.Cost = 20f;
            LiteracyTeacher teacher = new LiteracyTeacher(humans, donor);
            teacher.TuitionPerIncome = 20f / 128f;
            teacher.MinTuition = 10f;
            donor.OptionMenu.Pratham.Funded = true;
            teacher.TryTeachEach();
            Assert.AreEqual(1f, teacher.LiteracyRate);
            Assert.IsTrue(donor.OptionMenu.Pratham.NoRoomForFunding);
        }

        [Test]
        public void TryTeachEach_MinTuition_LiteracyRate0()
        {
            List<Human> humans = new List<Human>{new Human{Income = 0f}};
            LiteracyTeacher teacher = new LiteracyTeacher(humans);
            teacher.TuitionPerIncome = 20f / 128f;
            teacher.MinTuition = 50f;
            teacher.TryTeachEach();
            Assert.AreEqual(0f, teacher.LiteracyRate);
        }
    }
}
