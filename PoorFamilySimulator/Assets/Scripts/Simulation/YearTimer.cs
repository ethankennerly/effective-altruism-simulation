using FineGameDesign.Events;
using System;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Simulation
{
    public sealed class YearTimer
    {
        public ActionEvent<string> TextChanged = new ActionEvent<string>();

        private int m_CurrentYear;
        private float m_FractionalYears;

        public YearTimer(int startingYear)
        {
            SetYear(startingYear);
        }

        public void AddYears(float years)
        {
            m_FractionalYears += years;
            if (m_FractionalYears < 1f && m_FractionalYears >= -1f)
            {
                return;
            }
            int yearsPassed = (int)m_FractionalYears;
            m_FractionalYears -= yearsPassed;
            SetYear(m_CurrentYear + yearsPassed);
        }

        private void SetYear(int currentYear)
        {
            if (currentYear == m_CurrentYear)
            {
                return;
            }

            m_CurrentYear = currentYear;
            TextChanged.SetValue(m_CurrentYear.ToString());
        }
    }
}
