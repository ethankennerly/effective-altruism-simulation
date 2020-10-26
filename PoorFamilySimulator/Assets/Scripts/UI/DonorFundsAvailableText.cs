using PoorFamily.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoorFamily.UI
{
    public sealed class DonorFundsAvailableText : ASimulatorObserver
    {
        [SerializeField] private TMP_Text m_FundsAvailableText = null;

        protected override void OnSimulatorUpdated(Simulator simulator)
        {
            m_FundsAvailableText.text = simulator.Donor.FundsAvailableString;
        }
    }
}
