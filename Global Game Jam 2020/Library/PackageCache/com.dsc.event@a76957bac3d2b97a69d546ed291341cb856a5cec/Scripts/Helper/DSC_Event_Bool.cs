using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_Bool : MonoBehaviour
    {
        #region Temp

        [System.Serializable]
        protected class EventBool : UnityEvent<bool> { }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected bool m_bBool;
        [SerializeField] protected EventBool m_hEvent;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Main

        public void SetBool(bool bBool)
        {
            m_bBool = bBool;
        }

        public void RunEvent()
        {
            m_hEvent?.Invoke(m_bBool);
        }

        public void RunEvent(bool bBool)
        {
            m_hEvent?.Invoke(bBool);
        }

        #endregion
    }
}