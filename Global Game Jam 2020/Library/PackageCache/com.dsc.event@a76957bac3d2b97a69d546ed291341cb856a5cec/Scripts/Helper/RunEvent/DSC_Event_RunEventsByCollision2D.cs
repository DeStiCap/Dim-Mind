using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByCollision2D : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Condition")]
        [SerializeField] protected EventCondition[] m_arrCondition;

        [Header("Event")]
        [SerializeField] protected UnityEvent m_hEnterEvent;
        [SerializeField] protected UnityEvent m_hStayEvent;
        [SerializeField] protected UnityEvent m_hExitEvent;

        [Header("Event Send Data")]
        [SerializeField] protected EventGameObject m_hEnterEventGameObject;
        [SerializeField] protected EventGameObject m_hStayEventGameObject;
        [SerializeField] protected EventGameObject m_hExitEventGameObject;

#pragma warning restore 0649
        #endregion

        protected EventConditionData m_hConditionData;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hConditionData = new EventConditionData(transform);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsPassCondition())
                return;

            m_hEnterEvent?.Invoke();
            m_hEnterEventGameObject?.Invoke(collision.gameObject);
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            if (!IsPassCondition())
                return;

            m_hStayEvent?.Invoke();
            m_hStayEventGameObject?.Invoke(collision.gameObject);
        }

        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            if (!IsPassCondition())
                return;

            m_hExitEvent?.Invoke();
            m_hExitEventGameObject?.Invoke(collision.gameObject);
        }

        #endregion

        #region Events

        public void SetCondition(params EventCondition[] arrCondition)
        {
            m_arrCondition = arrCondition;
        }

        public void SetEnterEvent(UnityEvent hEvent)
        {
            m_hEnterEvent = hEvent;
        }

        public void SetEnterEvent(EventGameObject hEvent)
        {
            m_hEnterEventGameObject = hEvent;
        }

        public void SetStayEvent(UnityEvent hEvent)
        {
            m_hStayEvent = hEvent;
        }

        public void SetStayEvent(EventGameObject hEvent)
        {
            m_hStayEventGameObject = hEvent;
        }

        public void SetExitEvent(UnityEvent hEvent)
        {
            m_hExitEvent = hEvent;
        }

        public void SetExitEvent(EventGameObject hEvent)
        {
            m_hExitEventGameObject = hEvent;
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