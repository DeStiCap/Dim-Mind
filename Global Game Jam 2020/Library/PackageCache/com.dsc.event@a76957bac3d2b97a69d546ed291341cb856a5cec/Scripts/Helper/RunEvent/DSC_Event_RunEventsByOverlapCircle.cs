using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByOverlapCircle : BaseRunEventByOverlap
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Overlap")]
        [SerializeField] protected Transform m_hOverlapPosition;
        [Min(0)]
        [SerializeField] protected float m_fRadius = 1f;
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
        public override Transform overlapPosition { get { return m_hOverlapPosition; } set { m_hOverlapPosition = value; } }
        protected override bool checkUpdate { get { return m_bCheckUpdate; } set { m_bCheckUpdate = value; } }
        protected override UpdateType updateType { get { return m_eUpdateType; } set { m_eUpdateType = value; } }
        protected override GameObject[] ignoreTargetArray { get { return m_arrIgnoreTarget; } set { m_arrIgnoreTarget = value; } }
        protected override EventCondition[] eventConditionArray { get { return m_arrCondition; } set { m_arrCondition = value; } }
        protected override UnityEvent hitEvent { get { return m_hHitEvent; } set { m_hHitEvent = value; } }
        protected override UnityEvent notHitEvent { get { return m_hNotHitEvent; } set { m_hNotHitEvent = value; } }
        protected override EventGameObject hitEventGameObject { get { return m_hHitEventGameObject; } set { m_hHitEventGameObject = value; } }

        protected Collider2D[] hitColArray
        {
            get
            {
                if (m_arrHitCol == null)
                    m_arrHitCol = new Collider2D[m_nCheckArraySize];

                return m_arrHitCol;
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

        protected Collider2D[] m_arrHitCol;

        #endregion

        #region Base - Mono

        protected virtual void OnValidate()
        {
            if (hitColArray.Length != m_nCheckArraySize)
            {
                m_arrHitCol = new Collider2D[m_nCheckArraySize];
            }
        }

        #endregion

        #region Events

        public override void CheckOverlap()
        {
            if (!m_arrCondition.PassAllCondition(eventConditionData))
                return;

            Vector2 vPos = m_hOverlapPosition != null ? m_hOverlapPosition.position : transform.position;
            int nHit = Physics2D.OverlapCircleNonAlloc(vPos, m_fRadius, hitColArray, m_eCheckLayer);

            if (nHit <= 0)
            {
                m_hNotHitEvent?.Invoke();
                return;
            }

            bool bHit = false;
            int nSendCount = 0;

            for (int i = 0; i < nHit; i++)
            {
                var hHitGo = m_arrHitCol[i].gameObject;
                if (CheckNotIgnoreTarget(hHitGo))
                {
                    if(!bHit)
                        m_hHitEvent?.Invoke();

                    bHit = true;

                    m_hHitEventGameObject?.Invoke(hHitGo);
                    nSendCount++;

                    if(nSendCount >= m_nMaxSendData)
                        break;
                }
            }

            if(!bHit)
                m_hNotHitEvent?.Invoke();
        }

        public void SetRadius(float fRadius)
        {
            m_fRadius = fRadius;
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
            Vector2 vPos = m_hOverlapPosition != null ? m_hOverlapPosition.position : transform.position;
            UnityEditor.Handles.color = m_hGizmosColor;
            UnityEditor.Handles.DrawWireDisc(vPos, Vector3.forward, m_fRadius);
        }

#endif
        #endregion
    }
}