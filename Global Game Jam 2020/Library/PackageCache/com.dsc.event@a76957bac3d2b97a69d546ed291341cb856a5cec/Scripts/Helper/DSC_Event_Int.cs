using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_Int : MonoBehaviour
    {
        #region Temp

        [System.Serializable]
        protected class EventInt : UnityEvent<int> { }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nInt;
        [SerializeField] protected EventInt m_hEvent;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Main

        public void SetInt(int nInt)
        {
            m_nInt = nInt;
        }

        public void SetInt(float fInt)
        {
            m_nInt = Mathf.RoundToInt(fInt);
        }

        public void SetInt(string sInt)
        {
            if (!int.TryParse(sInt, out int nResult))
                return;

            m_nInt = nResult;
        }

        public void RunEvent()
        {
            m_hEvent?.Invoke(m_nInt);
        }

        public void RunEvent(int nInt)
        {
            m_hEvent?.Invoke(nInt);
        }

        public void RunEvent(float fInt)
        {
            m_hEvent?.Invoke(Mathf.RoundToInt(fInt));
        }

        public void RunEvent(string sInt)
        {
            if (!int.TryParse(sInt, out int nResult))
                return;

            m_hEvent?.Invoke(nResult);
        }

        #endregion
    }
}