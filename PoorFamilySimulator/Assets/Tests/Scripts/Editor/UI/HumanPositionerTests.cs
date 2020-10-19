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

        [Test]
        public void AveragePosition_6400And1600ThenAddTime_LerpHalfwayFromPoorestToRichest()
        {
            Simulator simulator = new Simulator();
            HumanPositioner humanPositioner = new HumanPositioner();
            humanPositioner.HumanPositionRoot = new GameObject().transform;
            humanPositioner.PoorestPosition = new GameObject().transform;
            humanPositioner.RichestPosition = new GameObject().transform;
            humanPositioner.AveragePosition = new GameObject().transform;

            SetLocalX(humanPositioner.PoorestPosition, -6f);
            SetLocalX(humanPositioner.RichestPosition, 4f);
            SetLocalX(humanPositioner.HumanPositionRoot, 1f);

            Human income6400 = new Human{Income = 6400};
            Human income1600 = new Human{Income = 1600};
            simulator.Humans.Add(income6400);
            simulator.Humans.Add(income1600);
            simulator.AddTime(0.001f);
            humanPositioner.UpdatePositions(simulator);
            Assert.AreEqual(2f, humanPositioner.AveragePosition.localPosition.x);
        }

        private static void SetLocalX(Transform transform, float nextX)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = nextX;
            transform.localPosition = localPosition;
        }
    }
}
