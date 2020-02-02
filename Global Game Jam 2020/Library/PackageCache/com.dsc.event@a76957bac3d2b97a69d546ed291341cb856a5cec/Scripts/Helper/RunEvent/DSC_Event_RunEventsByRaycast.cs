using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByRaycast : BaseRunEventByRaycast
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Raycast")]
        [SerializeField] protected Transform m_hOriginPosition;
        [SerializeField] protected Vector3 m_vDirection;
        [Min(0)]
        [SerializeField] protected float m_fDistance;
        [SerializeField] protected LayerMask m_eCheckLayer = -1;

        [Header("Option")]
        [SerializeField] protected GameObject[] m_arrIgnoreTarget;
        [SerializeField] protected bool m_bCheckUpdate = true;
        [SerializeField] protected UpdateType m_eUpdateType = UpdateType.FixedUpdate;
        [Min(0)]
        [SerializeField] protected int m_nCheckArraySize = 3;

        [Header("Event")]
        [SerializeField] protected EventCondition[] m_arrCondition;
        [SerializeField] protected UnityEvent m_hHitEvent;          
        [SerializeField] protected UnityEvent m_hNotHitEvent;

        [Header("Event Send Data")]
        [Min(1)]
        [SerializeField] protected int m_nMaxSendData = 1;
        [SerializeField] protected EventGameObject m_hHitEventGameObject;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] protected bool m_bAlwayShowGizmos;
        [SerializeField] protected Color m_hGizmosColor = Color.green;
#endif

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        protected override EventConditionData eventConditionData { get; set; }
        protected override GameObject[] ignoreTargetArray { get { return m_arrIgnoreTarget; } set { m_arrIgnoreTarget = value; } }
        protected override bool checkUpdate { get { return m_bCheckUpdate; } set { m_bCheckUpdate = value; } }
        protected override UpdateType updateType { get { return m_eUpdateType; } set { m_eUpdateType = value; } }
        protected override UnityEvent hitEvent { get { return m_hHitEvent; } set { m_hHitEvent = value; } }
        protected override UnityEvent notHitEvent { get { return m_hNotHitEvent; } set { m_hNotHitEvent = value; } }
        protected override EventGameObject hitEventGameObject { get { return m_hHitEventGameObject; } set { m_hHitEventGameObject = value; } }

        protected RaycastHit[] rayHitArray
        {
            get
            {
                if (m_arrRayHit == null)
                    m_arrRayHit = new RaycastHit[m_nCheckArraySize];

                return m_arrRayHit;
            }
        }

        public int maxSendData
        {
            get { return m_nMaxSendData; }
            set
            {
                m_nMaxSendData = value;
                if (m_nMaxSendData < 1)
                    m_nMaxSendData = 1;
            }
        }

        #endregion

        protected RaycastHit[] m_arrRayHit;
        
        #endregion

        #region Base - Mono

        protected virtual void OnValidate()
        {
            if(rayHitArray.Length != m_nCheckArraySize)
            {
                m_arrRayHit = new RaycastHit[m_nCheckArraySize];
            }
        }

        #endregion

        #region Events

        public override void CheckRaycast()
        {
            if (!m_arrCondition.PassAllCondition(eventConditionData))
                return;

            Vector3 vPos = m_hOriginPosition != null ? m_hOriginPosition.position : transform.position;
            int nHit = Physics.RaycastNonAlloc(vPos, m_vDirection, rayHitArray, m_fDistance, m_eCheckLayer);

            if(nHit <= 0)
            {
                m_hNotHitEvent?.Invoke();
                return;
            }

            bool bHit = false;
            int nSendCount = 0;

            for(int i = 0; i < nHit; i++)
            {
                var hHitGO = m_arrRayHit[i].collider.gameObject;
                if (CheckNotIgnoreTarget(hHitGO))
                {
                    if (!bHit)
                        m_hHitEvent?.Invoke();

                    bHit = true;

                    m_hHitEventGameObject?.Invoke(hHitGO);
                    nSendCount++;

                    if (nSendCount >= m_nMaxSendData)
                        break;
                }
            }

            if(!bHit)
                m_hNotHitEvent?.Invoke();
        }

        #endregion

        #region Editor Only
#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            if (!m_bAlwayShowGizmos)
                return;

            ShowGizmos();
        }

        void OnDrawGizmosSelected()
        {
            if (m_bAlwayShowGizmos)
                return;

            ShowGizmos();
        }

        void ShowGizmos()
        {
            Vector3 vPos = m_hOriginPosition != null ? m_hOriginPosition.position : transform.position;
            Vector3 vDestination = vPos + m_vDirection * m_fDistance;

            Gizmos.color = m_hGizmosColor;
            Gizmos.DrawLine(vPos, vDestination);
        }

#endif
        #endregion
    }
}