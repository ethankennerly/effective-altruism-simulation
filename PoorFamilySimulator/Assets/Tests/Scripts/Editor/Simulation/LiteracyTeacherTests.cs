using NUnit.Framework;
using PoorFamily.Simulation;
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
            teacher.AddFunds(79f);
            teacher.TryTeachEach();
            Assert.AreEqual(0.75f, teacher.LiteracyRate);
        }
    }
}
