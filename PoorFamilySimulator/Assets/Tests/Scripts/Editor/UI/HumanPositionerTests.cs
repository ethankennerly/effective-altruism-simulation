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
            HumanPositioner humanPositioner = SetUpHumanPositioner();
            humanPositioner.UpdatePositions(simulator);
            Assert.AreEqual(new List<GameObject>(), humanPositioner.HumanTransforms);
        }

        [Test]
        public void AverageTransform_6400And1600ThenAddTime_LerpHalfwayFromPoorestToRichest()
        {
            Simulator simulator = new Simulator();
            HumanPositioner humanPositioner = SetUpHumanPositioner();

            Human income6400 = new Human{Income = 6400};
            Human income1600 = new Human{Income = 1600};
            simulator.Humans.Add(income6400);
            simulator.Humans.Add(income1600);
            simulator.AddTime(0.001f);
            humanPositioner.UpdatePositions(simulator);
            Assert.AreEqual(-1f, humanPositioner.AverageTransform.localPosition.x, "Average");
            Assert.AreEqual(2, humanPositioner.HumanTransforms.Count, "Number of Human Transforms");
            Assert.AreEqual(0f, humanPositioner.HumanTransforms[0].localPosition.x, "Income 6400");
            Assert.AreEqual(-2f, humanPositioner.HumanTransforms[1].localPosition.x, "Income 1600");
        }

        private static HumanPositioner SetUpHumanPositioner()
        {
            HumanPositioner humanPositioner = new HumanPositioner();
            humanPositioner.HumanRoot = new GameObject().transform;
            humanPositioner.PoorestTransform = new GameObject().transform;
            humanPositioner.RichestTransform = new GameObject().transform;
            humanPositioner.AverageTransform = new GameObject().transform;

            SetLocalX(humanPositioner.PoorestTransform, -6f);
            SetLocalX(humanPositioner.RichestTransform, 4f);
            SetLocalX(humanPositioner.HumanRoot, 1f);
            return humanPositioner;
        }

        private static void SetLocalX(Transform transform, float nextX)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = nextX;
            transform.localPosition = localPosition;
        }
    }
}
