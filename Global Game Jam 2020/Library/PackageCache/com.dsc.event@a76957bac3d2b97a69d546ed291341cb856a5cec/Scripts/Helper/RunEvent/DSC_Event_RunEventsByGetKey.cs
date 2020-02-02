using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByGetKey : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected KeyCode m_ekey;
        
        [Header("Option")]
        [SerializeField] protected GetInputType m_eType = GetInputType.Down;
        [Min(0)]
        [SerializeField] protected float m_fHoldTime;

        [Header("Condition")]
        [SerializeField] protected EventCondition[] m_arrCondition;

        [Header("Event")]
        [SerializeField] protected UnityEvent[] m_arrEvent;
        
        [Header("Runtime Data")]
        [Min(0)]
        [SerializeField] protected int m_nCurrentIndex;

#pragma warning restore 0649
        #endregion

        protected EventConditionData m_hConditionData;
        protected float m_fHoldingTime;

        #endregion

        #region Base - Mono

        protected virtual void OnValidate()
        {
            if (m_arrEvent != null)
            {
                if (m_arrEvent.Length > 0 && m_nCurrentIndex >= m_arrEvent.Length)
                {
                    m_nCurrentIndex = m_arrEvent.Length - 1;
                }
                else if (m_arrEvent.Length == 0)
                    m_nCurrentIndex = 0;
            }
        }

        protected virtual void Awake()
        {
            m_hConditionData = new EventConditionData(transform);
        }

        protected virtual void Update()
        {
            bool bHold = false;

            if (m_arrEvent == null || m_arrEvent.Length <= 0)
                return;

            var hEvent = m_arrEvent[m_nCurrentIndex];

            switch (m_eType)
            {
                case GetInputType.Down:
                    if (Input.GetKeyDown(m_ekey))
                    {
                        if (IsPassCondition())
                        {
                            hEvent?.Invoke();
                            NextIndex();
                        }
                    }
                    break;

                case GetInputType.Up:
                    if (Input.GetKeyUp(m_ekey))
                    {
                        if (IsPassCondition())
                        {
                            hEvent?.Invoke();
                            NextIndex();
                        }
                    }
                    break;

                case GetInputType.HoldEnd:
                    if (Input.GetKey(m_ekey))
                    {
                        bHold = true;
                    }
                    break;
            }

            if (bHold)
            {
                if (m_fHoldingTime >= m_fHoldTime)
                {
                    if (IsPassCondition())
                    {
                        hEvent?.Invoke();
                        m_fHoldingTime = 0;
                        NextIndex();
                    }
                }
                else
                    m_fHoldingTime += Time.deltaTime;
            }
            else
            {
                m_fHoldingTime = 0;
            }
        }

        #endregion

        #region Events

        public virtual void SetCondition(params EventCondition[] arrCondition)
        {
            m_arrCondition = arrCondition;
        }

        public virtual void SetEvent(params UnityEvent[] arrEvent)
        {
            m_arrEvent = arrEvent;
        }

        public virtual void SetGetKey(KeyCode eKey)
        {
            m_ekey = eKey;
        }

        public virtual void SetCurrentIndex(int nIndex)
        {
            if (nIndex < 0 || m_arrEvent == null)
                nIndex = 0;
            else if (nIndex >= m_arrEvent.Length)
                nIndex = m_arrEvent.Length - 1;

            m_nCurrentIndex = nIndex;
        }

        public virtual void SetHoldTime(float fHoldTime)
        {
            if (fHoldTime < 0)
                fHoldTime = 0;

            m_fHoldTime = fHoldTime;
        }

        #endregion

        #region Helper

        protected bool IsPassCondition()
        {
            return m_arrCondition.PassAllCondition(m_hConditionData);
        }

        protected virtual void NextIndex()
        {
            m_nCurrentIndex++;
            if (m_nCurrentIndex >= m_arrEvent.Length)
                m_nCurrentIndex = 0;
        }

        #endregion
    }
}