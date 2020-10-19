using PoorFamily.Simulation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.UI
{
    [Serializable]
    public sealed class HumanPositioner
    {
        public GameObject HumanPrefab = null;
        public Transform HumanRoot = null;
        public Transform PoorestTransform = null;
        public Transform RichestTransform = null;
        public Transform AverageTransform = null;
        public List<Transform> HumanTransforms = null;

        public void UpdatePositions(Simulator simulator)
        {
            if (HumanTransforms == null)
            {
                HumanTransforms = new List<Transform>();
            }

            float lerpAmount = simulator.NormalizedWealth;
            Vector3 normalizedPosition = Vector3.Lerp(
                PoorestTransform.localPosition, RichestTransform.localPosition, lerpAmount);
            AverageTransform.localPosition = normalizedPosition;
        }
    }
}

