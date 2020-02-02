using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_String : MonoBehaviour
    {
        #region Temp

        [System.Serializable]
        protected class EventString : UnityEvent<string> { }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected string m_sString;
        [SerializeField] protected EventString m_hEvent;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Main

        public void SetString(string sString)
        {
            m_sString = sString;
        }

        public void SetString(int nString)
        {
            m_sString = nString.ToString();
        }

        public void SetString(float fString)
        {
            m_sString = fString.ToString();
        }

        public void RunEvent()
        {
            m_hEvent?.Invoke(m_sString);
        }

        public void RunEvent(string sString)
        {
            m_hEvent?.Invoke(sString);
        }

        public void RunEvent(int nString)
        {
            m_hEvent?.Invoke(nString.ToString());
        }

        public void RunEvent(float fString)
        {
            m_hEvent?.Invoke(fString.ToString());
        }

        #endregion

    }
}