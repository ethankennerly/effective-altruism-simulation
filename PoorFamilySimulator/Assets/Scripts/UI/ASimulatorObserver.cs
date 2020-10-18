using PoorFamily.Simulation;
using System;
using UnityEngine;

namespace PoorFamily.UI
{
    public abstract class ASimulatorObserver : MonoBehaviour
    {
        [SerializeField] private SimulatorInspector m_SimulatorInspector = null;

        private Action<Simulator> m_OnSimulatorUpdated;

        private void Awake()
        {
            m_OnSimulatorUpdated = OnSimulatorUpdated;
        }

        private void OnEnable()
        {
            m_SimulatorInspector.Simulator.Updated.OnInvoke += m_OnSimulatorUpdated;
        }

        private void OnDisable()
        {
            m_SimulatorInspector.Simulator.Updated.OnInvoke -= m_OnSimulatorUpdated;
        }

        protected virtual void OnSimulatorUpdated(Simulator simulator)
        {
        }
    }
}
