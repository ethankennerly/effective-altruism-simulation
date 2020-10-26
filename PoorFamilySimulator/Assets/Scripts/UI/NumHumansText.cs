using PoorFamily.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoorFamily.UI
{
    public class NumHumansText : ASimulatorObserver
    {
        [SerializeField] private TMP_Text m_NumHumansText = null;

        protected override void OnSimulatorUpdated(Simulator simulator)
        {
            string numHumansString = simulator.NumHumansString;
            if (m_NumHumansText.text == numHumansString)
            {
                return;
            }

            m_NumHumansText.text = numHumansString;
        }
    }
}
