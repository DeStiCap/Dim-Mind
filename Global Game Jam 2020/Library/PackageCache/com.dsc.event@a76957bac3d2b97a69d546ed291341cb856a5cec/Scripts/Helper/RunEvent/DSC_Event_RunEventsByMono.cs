using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByMono : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Condition")]
        [SerializeField] protected EventCondition[] m_arrCondition;

        [Header("Events")]
        [SerializeField] protected UnityEvent m_hOnAwake;
        [SerializeField] protected UnityEvent m_hOnStart;
        [SerializeField] protected UnityEvent m_hOnEnable;
        [SerializeField] protected UnityEvent m_hOnDisable;
        [SerializeField] protected UnityEvent m_hOnDestroy;        

#pragma warning restore 0649
        #endregion

        [SerializeField] protected EventConditionData m_hConditionData;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hConditionData = new EventConditionData(transform);

            if (IsPassCondition())
                m_hOnAwake.Invoke();
        }

        protected virtual void Start()
        {
            if (IsPassCondition())
                m_hOnStart.Invoke();
        }

        protected virtual void OnEnable()
        {
            if (IsPassCondition())
                m_hOnEnable.Invoke();
        }

        protected virtual void OnDisable()
        {
            if(IsPassCondition())
                m_hOnDisable.Invoke();
        }

        protected virtual void OnDestroy()
        {
            if(IsPassCondition())
                m_hOnDestroy.Invoke();
        }

        #endregion

        #region Main

        public virtual void SetCondition(params EventCondition[] arrCondition)
        {
            m_arrCondition = arrCondition;
        }

        public virtual void SetEventOnAwake(UnityEvent hEvent)
        {
            m_hOnAwake = hEvent;
        }

        public virtual void SetEventOnStart(UnityEvent hEvent)
        {
            m_hOnStart = hEvent;
        }

        public virtual void SetEventOnEnable(UnityEvent hEvent)
        {
            m_hOnEnable = hEvent;
        }

        public virtual void SetEventOnDisable(UnityEvent hEvent)
        {
            m_hOnDisable = hEvent;
        }

        public virtual void SetEventOnDestroy(UnityEvent hEvent)
        {
            m_hOnDestroy = hEvent;
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