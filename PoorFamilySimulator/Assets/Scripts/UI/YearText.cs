using System;
using TMPro;
using UnityEngine;

namespace PoorFamily.UI
{
    public sealed class YearText : MonoBehaviour
    {
        [SerializeField] private MainInspector m_MainInspector = null;
        [SerializeField] private TMP_Text m_YearText = null;

        private Action<string> m_OnTextChanged;

        private void Awake()
        {
            m_OnTextChanged = SetYearText;
        }

        private void OnEnable()
        {
            m_MainInspector.Simulator.YearTimer.TextChanged.OnInvoke += m_OnTextChanged;
        }

        private void OnDisable()
        {
            m_MainInspector.Simulator.YearTimer.TextChanged.OnInvoke -= m_OnTextChanged;
        }

        private void SetYearText(string nextYearText)
        {
            m_YearText.text = nextYearText;
        }
    }
}
