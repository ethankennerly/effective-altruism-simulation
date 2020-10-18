using System;
using Debug = UnityEngine.Debug;

namespace FineGameDesign.Events
{
    public class ActionEvent<T>
    {
        private event Action<T> m_OnInvoke;
        public event Action<T> OnInvoke
        {
            add
            {
                if (value == null)
                {
                    return;
                }

                Action<T> action = value;
                m_OnInvoke -= action;
                m_OnInvoke += action;
                action(m_Value);
            }
            remove
            {
                m_OnInvoke -= value;
            }
        }

        private T m_Value;

        public T GetValue()
        {
            return m_Value;
        }

        public void SetValue(T nextValue)
        {
            if (m_Value == null && nextValue == null)
            {
                return;
            }

            if (m_Value != null && m_Value.Equals(nextValue))
            {
                return;
            }

            m_Value = nextValue;
            TryInvoke(nextValue);
        }

        public void TryInvoke(T value)
        {
            if (m_OnInvoke == null)
            {
                return;
            }

            try
            {
                m_OnInvoke(value);
            }
            catch (Exception err)
            {
                Debug.LogError(err + "Failed to invoke.");
            }
        }
    }
}
