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
            PositionHumans(simulator);
            PositionAverage(simulator);
        }

        private void PositionHumans(Simulator simulator)
        {
            if (HumanTransforms == null)
            {
                HumanTransforms = new List<Transform>();
            }

            int numHumans = simulator.Humans.Count;
            for (int numTransforms = HumanTransforms.Count; numTransforms < numHumans; ++numTransforms)
            {
                GameObject humanObject = UnityEngine.Object.Instantiate(HumanPrefab, HumanRoot);
                HumanTransforms.Add(humanObject.transform);
            }

            int humanIndex = 0;
            foreach (Transform humanTransform in HumanTransforms)
            {
                float lerpAmount = simulator.Humans[humanIndex].NormalizedWealth;
                Vector3 humanPosition = Vector3.Lerp(
                    PoorestTransform.localPosition, RichestTransform.localPosition, lerpAmount);
                humanTransform.localPosition = humanPosition;
                humanIndex++;
            }
        }

        private void PositionAverage(Simulator simulator)
        {
            float lerpAmount = simulator.NormalizedWealth;
            Vector3 normalizedPosition = Vector3.Lerp(
                PoorestTransform.localPosition, RichestTransform.localPosition, lerpAmount);
            AverageTransform.localPosition = normalizedPosition;
        }
    }
}

