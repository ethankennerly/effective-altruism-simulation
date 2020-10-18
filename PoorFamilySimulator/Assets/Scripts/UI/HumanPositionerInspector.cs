using PoorFamily.Simulation;
using System.Collections.Generic;
using UnityEngine;

namespace PoorFamily.UI
{
    public sealed class HumanPositionerInspector : ASimulatorObserver
    {
        [SerializeField] private HumanPositioner m_HumanPositioner = new HumanPositioner();

        protected override void OnSimulatorUpdated(Simulator simulator)
        {
            m_HumanPositioner.UpdatePositions(simulator);
        }
    }
}
