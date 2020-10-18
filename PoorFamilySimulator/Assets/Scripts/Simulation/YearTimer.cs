using FineGameDesign.Events;
using System;

using Debug = UnityEngine.Debug;

namespace PoorFamily.Simulation
{
    public sealed class YearTimer
    {
        public ActionEvent<string> TextChanged = new ActionEvent<string>();

        private int m_CurrentYear;

        private int m_EndYear;

        private float m_FractionalYears;

        public YearTimer(int startingYear, int yearsRemaining)
        {
            m_EndYear = startingYear + yearsRemaining;
            SetYear(startingYear);
        }

        public int GetYear()
        {
            return m_CurrentYear;
        }

        public void AddYears(float additionalYears)
        {
            if (additionalYears == 0f)
            {
                return;
            }

            if (m_CurrentYear >= m_EndYear && additionalYears > 0f)
            {
                return;
            }

            m_FractionalYears += additionalYears;
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
            if (currentYear >= m_EndYear)
            {
                currentYear = m_EndYear;
            }

            if (currentYear == m_CurrentYear)
            {
                return;
            }

            m_CurrentYear = currentYear;
            TextChanged.SetValue(m_CurrentYear.ToString());
        }
    }
}
