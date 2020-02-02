using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByEvent : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected EventCondition[] m_arrCondition;
        [SerializeField] protected UnityEvent preRunEvents;
        [SerializeField] protected UnityEvent runEvents;
        [SerializeField] protected UnityEvent postRunEvents;

#pragma warning restore 0649
        #endregion

        protected EventConditionData m_hConditionData;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hConditionData = new EventConditionData(transform);
        }

        #endregion

        #region Main

        public virtual void RunEvents()
        {
            if (!IsPassCondition())
                return;

            preRunEvents?.Invoke();
            runEvents?.Invoke();
            postRunEvents?.Invoke();
        }

        public void SetCondition(params EventCondition[] arrCondition)
        {
            m_arrCondition = arrCondition;
        }

        public void SetPreRunEvents(UnityEvent hEvent)
        {
            preRunEvents = hEvent;
        }

        public void SetRunEvents(UnityEvent hEvent)
        {
            runEvents = hEvent;
        }

        public void SetPostRunEvents(UnityEvent hEvent)
        {
            postRunEvents = hEvent;
        }

        #endregion

        #region Helper

        protected bool IsPassCondition()
        {
            return m_arrCondition.PassAllCondition(m_hConditionData);
        }

        #endregion
    }
}