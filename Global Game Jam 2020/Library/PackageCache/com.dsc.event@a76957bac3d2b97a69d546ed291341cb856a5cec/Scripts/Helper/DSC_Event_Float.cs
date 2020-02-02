using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_Float : MonoBehaviour
    {
        #region Temp

        [System.Serializable]
        protected class EventFloat : UnityEvent<float> { }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected float m_fFloat;
        [SerializeField] protected EventFloat m_hEvent;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Main

        public void SetFloat(float fFloat)
        {
            m_fFloat = fFloat;
        }

        public void SetFloat(int nFloat)
        {
            m_fFloat = nFloat;
        }

        public void SetFloat(string sFloat)
        {
            if (!float.TryParse(sFloat, out float fResult))
                return;

            m_fFloat = fResult;
        }

        public void RunEvent()
        {
            m_hEvent?.Invoke(m_fFloat);
        }

        public void RunEvent(float fFloat)
        {
            m_hEvent?.Invoke(fFloat);
        }

        public void RunEvent(int nFloat)
        {
            m_hEvent?.Invoke(nFloat);
        }

        public void RunEvent(string sFloat)
        {
            if (!float.TryParse(sFloat, out float fResult))
                return;

            m_hEvent?.Invoke(fResult);
        }

        #endregion
    }
}