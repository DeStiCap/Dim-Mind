using UnityEngine;

namespace DSC.Event.Helper
{
    public class DSC_Event_AddForce : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Rigidbody m_hTarget;
        [SerializeField] protected float m_fForce;
        [SerializeField] protected Vector3 m_vForceDirection;
        [SerializeField] protected ForceMode m_eMode;

        [Header("Debug")]
        [SerializeField] protected bool m_bShowDebugLog;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public Rigidbody target
        {
            get
            {
                if (m_hTarget == null)
                    m_hTarget = GetComponent<Rigidbody>();

                return m_hTarget;
            }
            set { m_hTarget = value; }
        }

        public float force
        {
            get { return m_fForce; }
            set { m_fForce = value; }
        }

        public float directionX
        {
            get { return m_vForceDirection.x; }
            set { m_vForceDirection.x = value; }
        }

        public float directionY
        {
            get { return m_vForceDirection.y; }
            set { m_vForceDirection.y = value; }
        }

        public float directionZ
        {
            get { return m_vForceDirection.z; }
            set { m_vForceDirection.z = value; }
        }

        public ForceMode mode
        {
            get { return m_eMode; }
            set { m_eMode = value; }
        }

        #endregion

        #endregion

        #region Events

        public void AddForce()
        {
            MainAddForce(m_fForce);
        }

        public void AddForce(float fAddForce)
        {
            MainAddForce(fAddForce);
        }

        public void AddForce(GameObject hTarget)
        {
            SetTarget(hTarget);
            AddForce();
        }

        public void SetTarget(GameObject hTarget)
        {
            if (hTarget == null)
                m_hTarget = null;

            m_hTarget = hTarget.GetComponent<Rigidbody>();
        }

        public void ResetDirectionToZero()
        {
            m_vForceDirection = Vector3.zero;
        }

        #endregion

        #region Main

        protected virtual void MainAddForce(float fAddForce)
        {
            Rigidbody hRigid = target;

            if (hRigid == null)
            {
                if(m_bShowDebugLog)
                    Debug.LogWarning("Don't have target rigidbody to add force.");
                return;
            }

            hRigid.AddForce(fAddForce * m_vForceDirection,m_eMode);
        }

        #endregion
    }
}