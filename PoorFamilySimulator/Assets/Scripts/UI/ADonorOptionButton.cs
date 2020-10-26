using PoorFamily.Simulation;
using PoorFamily.Simulation.Donation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoorFamily.UI
{
    public abstract class ADonorOptionButton : ASimulatorObserver
    {
        [SerializeField] private Button m_SelectButton = null;
        [SerializeField] private TMP_Text m_CostText = null;

        protected ADonorOption m_DonorOption;

        protected override void OnEnable()
        {
            m_CostText.text = m_DonorOption.CostString;
            m_SelectButton.onClick.AddListener(m_DonorOption.Select);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            m_SelectButton.onClick.RemoveListener(m_DonorOption.Select);
            base.OnEnable();
        }

        protected override void OnSimulatorUpdated(Simulator simulator)
        {
            m_SelectButton.interactable = !m_DonorOption.WillFund;
        }
    }
}
