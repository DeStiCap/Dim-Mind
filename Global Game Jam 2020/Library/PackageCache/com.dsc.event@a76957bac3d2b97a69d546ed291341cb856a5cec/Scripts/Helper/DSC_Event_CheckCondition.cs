using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_CheckCondition : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected EventCondition[] m_arrCondition;

        [Header("Success Event")]
        [SerializeField] protected UnityEvent m_hPreSuccess;
        [SerializeField] protected UnityEvent m_hSuccess;
        [SerializeField] protected UnityEvent m_hPostSuccess;

        [Header("Fail Event")]
        [SerializeField] protected UnityEvent m_hPreFail;
        [SerializeField] protected UnityEvent m_hFail;
        [SerializeField] protected UnityEvent m_hPostFail;

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

        public virtual void RunEvent()
        {
            if (m_arrCondition.PassAllCondition(m_hConditionData))
            {
                m_hPreSuccess?.Invoke();
                m_hSuccess?.Invoke();
                m_hPostSuccess?.Invoke();
            }
            else
            {
                m_hPreFail?.Invoke();
                m_hFail?.Invoke();
                m_hPostFail?.Invoke();
            }
        }

        public virtual void SetCondition(params EventCondition[] arrCondition)
        {
            m_arrCondition = arrCondition;
        }

        public virtual void SetPreSuccess(UnityEvent hEvent)
        {
            m_hPreSuccess = hEvent;
        }

        public virtual void SetSuccess(UnityEvent hEvent)
        {
            m_hSuccess = hEvent;
        }

        public virtual void SetPostSuccess(UnityEvent hEvent)
        {
            m_hPostSuccess = hEvent;
        }

        public virtual void SetPreFail(UnityEvent hEvent)
        {
            m_hPreFail = hEvent;
        }

        public virtual void SetFail(UnityEvent hEvent)
        {
            m_hFail = hEvent;
        }

        public virtual void SetPostFail(UnityEvent hEvent)
        {
            m_hPostFail = hEvent;
        }

        #endregion
    }
}