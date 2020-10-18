using System;

using Debug = UnityEngine.Debug;

namespace PoorFamilySimulator.Simulation
{
    public sealed class YearTimer
    {
        private event Action<string> m_OnTextChanged;
        public event Action<string> OnTextChanged
        {
            add
            {
                m_OnTextChanged += value;
            }
            remove
            {
                m_OnTextChanged -= value;
            }
        }

        public YearTimer(int startingYear, float yearsPerSecond)
        {
            Debug.LogWarning("TODO: Construct Year Timer.");
        }

        public void AddYears(float years)
        {
            Debug.LogWarning("TODO: Add Years.");
        }
    }
}
