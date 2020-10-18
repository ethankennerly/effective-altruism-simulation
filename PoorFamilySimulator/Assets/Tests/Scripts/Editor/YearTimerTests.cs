using NUnit.Framework;
using PoorFamilySimulator.Simulation;
using System;

namespace PoorFamilySimulator.Simulation.Tests
{
    public sealed class YearTimerTests
    {
        [Test]
        public void OnTextChanged_2013ChangesTo2014()
        {
            YearTimer timer = new YearTimer(2013, 1f);
            string yearText = null;
            Action<string> setYearText = (nextYearText) => yearText = nextYearText;
            timer.OnTextChanged += setYearText;
            Assert.AreEqual("2013", yearText, "After adding a listener");

            timer.AddYears(0.5f);
            timer.AddYears(0.5f);
            Assert.AreEqual("2014", yearText, "After 1 year added");

            timer.OnTextChanged -= setYearText;
            timer.AddYears(1f);
            Assert.AreEqual("2014", yearText, "After removed");
        }
    }
}
