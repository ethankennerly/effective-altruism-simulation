using NUnit.Framework;
using PoorFamily.Simulation;
using System;

namespace PoorFamily.Simulation.Tests
{
    public sealed class YearTimerTests
    {
        [Test]
        public void TextChanged_2013ChangesTo2014()
        {
            YearTimer timer = new YearTimer(2013, 100);
            string yearText = null;
            Action<string> setYearText = (nextYearText) => yearText = nextYearText;
            timer.TextChanged.OnInvoke += setYearText;
            Assert.AreEqual("2013", yearText, "After adding a listener");

            timer.AddYears(0.5f);
            timer.AddYears(0.5f);
            Assert.AreEqual("2014", yearText, "After 1 year added");

            timer.TextChanged.OnInvoke -= setYearText;
            timer.AddYears(1f);
            Assert.AreEqual("2014", yearText, "After removed");
        }

        [Test]
        public void AddYears_200_ClampsTo100()
        {
            YearTimer timer = new YearTimer(2013, 100);
            timer.AddYears(200f);
            Assert.AreEqual(2113, timer.GetYear());
        }
    }
}
