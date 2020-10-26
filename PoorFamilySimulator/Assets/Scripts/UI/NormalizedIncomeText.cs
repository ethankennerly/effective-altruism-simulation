using PoorFamily.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoorFamily.UI
{
    public class NormalizedIncomeText : ASimulatorObserver
    {
        [SerializeField] private TMP_Text m_NormalizedIncomeText = null;

        protected override void OnSimulatorUpdated(Simulator simulator)
        {
            string normalizedIncomeString = simulator.Income.NormalizedIncomeString;
            if (m_NormalizedIncomeText.text == normalizedIncomeString)
            {
                return;
            }

            m_NormalizedIncomeText.text = normalizedIncomeString;
        }
    }
}
