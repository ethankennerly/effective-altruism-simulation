using NUnit.Framework;
using PoorFamily.Simulation;
using PoorFamily.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.UI.Tests
{
    public sealed class HumanPositionerTests
    {
        [Test]
        public void UpdatePositions_ZeroHumans_Zero()
        {
            Simulator simulator = new Simulator();
            HumanPositioner humanPositioner = new HumanPositioner();
            humanPositioner.UpdatePositions(simulator);
            Assert.AreEqual(new List<GameObject>(), humanPositioner.HumanObjects);
        }
    }
}
