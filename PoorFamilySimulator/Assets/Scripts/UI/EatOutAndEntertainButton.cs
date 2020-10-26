using PoorFamily.Simulation.Donation;
using UnityEngine;
using UnityEngine.UI;

namespace PoorFamily.UI
{
    public sealed class EatOutAndEntertainButton : ADonorOptionButton
    {
        protected override void OnEnable()
        {
            m_DonorOption = m_SimulatorInspector.Simulator.Donor.OptionMenu.EatOutAndEntertain;
            base.OnEnable();
        }
    }
}
