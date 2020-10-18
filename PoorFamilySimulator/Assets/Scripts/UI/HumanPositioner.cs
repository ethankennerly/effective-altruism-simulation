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
        public Transform HumanPositionRoot = null;
        public Transform PoorestPosition = null;
        public Transform RichestPosition = null;
        public Transform AveragePosition = null;
        public List<GameObject> HumanObjects = null;

        public void UpdatePositions(Simulator simulator)
        {
        }
    }
}

