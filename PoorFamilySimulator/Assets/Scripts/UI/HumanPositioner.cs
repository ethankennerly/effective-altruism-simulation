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
        public Transform Age0Transform = null;
        public Transform Age100Transform = null;
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

            for (int transformIndex = HumanTransforms.Count - 1; transformIndex > numHumans; --transformIndex)
            {
                Transform humanTransform = HumanTransforms[transformIndex];
                UnityEngine.Object.Destroy(humanTransform);
            }

            int humanIndex = 0;
            foreach (Transform humanTransform in HumanTransforms)
            {
                Human human = simulator.Humans[humanIndex];
                
                Vector3 humanPosition = Vector3.Lerp(
                    PoorestTransform.localPosition,
                    RichestTransform.localPosition,
                    human.NormalizedIncome);

                if (Age0Transform != null && Age100Transform != null)
                {
                    float normalizedAge = human.Age * 0.01f;
                    humanPosition.y = Mathf.Lerp(
                        Age0Transform.localPosition.y,
                        Age100Transform.localPosition.y,
                        normalizedAge);
                }
                
                humanTransform.localPosition = humanPosition;
                humanIndex++;
            }
        }

        private void PositionAverage(Simulator simulator)
        {
            float lerpAmount = simulator.NormalizedIncome;
            Vector3 normalizedPosition = Vector3.Lerp(
                PoorestTransform.localPosition, RichestTransform.localPosition, lerpAmount);
            AverageTransform.localPosition = normalizedPosition;
        }
    }
}

