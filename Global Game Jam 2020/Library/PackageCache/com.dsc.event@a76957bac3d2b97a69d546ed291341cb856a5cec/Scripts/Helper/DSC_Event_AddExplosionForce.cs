using UnityEngine;

namespace DSC.Event.Helper
{
    public class DSC_Event_AddExplosionForce : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Rigidbody m_hTarget;
        [SerializeField] protected float m_fForce;
        [Min(0)]
        [SerializeField] protected float m_fRadius;
        [SerializeField] protected float m_fUpwardsModifier;
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

        public float radius
        {
            get { return m_fRadius; }
            set
            {
                m_fRadius = value;
                if (m_fRadius < 0)
                    m_fRadius = 0;
            }
        }

        public float upwardsModifier
        {
            get { return m_fUpwardsModifier; }
            set { m_fUpwardsModifier = value; }
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
            MainAddExplosionForce(m_fForce);
        }

        public void AddExplosionForce(float fExplosionForce)
        {
            MainAddExplosionForce(fExplosionForce);
        }

        public void AddExplosionForce(GameObject hTarget)
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

        #endregion

        #region Main

        protected virtual void MainAddExplosionForce(float fAddForce)
        {
            Rigidbody hRigid = target;

            if (hRigid == null)
            {
                if(m_bShowDebugLog)
                    Debug.LogWarning("Don't have target rigidbody to add explosion force.");
                return;
            }

            Vector3 vPos = transform.position;
            hRigid.AddExplosionForce(m_fForce, vPos, m_fRadius, m_fUpwardsModifier,m_eMode);
        }

        #endregion
    }
}