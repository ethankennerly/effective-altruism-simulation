using PoorFamily.Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace PoorFamily.UI
{
    public sealed class GiveDirectlyButton : ASimulatorObserver
    {
        [SerializeField] private Button m_GiveButton = null;

        private IncomeSimulator m_Income;

        protected override void OnEnable()
        {
            m_Income = m_SimulatorInspector.Simulator.Income;
            m_GiveButton.onClick.AddListener(m_Income.GiveDirectly1205);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            m_GiveButton.onClick.RemoveListener(m_Income.GiveDirectly1205);
            base.OnEnable();
        }

        protected override void OnSimulatorUpdated(Simulator simulator)
        {
            m_GiveButton.interactable = !m_Income.IsTransferPending();
        }
    }
}
