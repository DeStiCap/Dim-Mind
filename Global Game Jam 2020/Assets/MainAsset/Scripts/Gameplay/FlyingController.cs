using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlyingController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Vector2 m_vFlyingDirection;
        [SerializeField] protected float m_fFlyingSpeed;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public Vector2 flyingDirection
        {
            get { return m_vFlyingDirection; }
            set { m_vFlyingDirection = value; }
        }

        public float flyingSpeed
        {
            get { return m_fFlyingSpeed; }
            set { m_fFlyingSpeed = value; }
        }

        #endregion

        Rigidbody2D m_hRigid;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hRigid = GetComponent<Rigidbody2D>();
            transform.SetParent(null);
        }

        protected virtual void OnEnable()
        {
            m_hRigid.velocity = m_vFlyingDirection * m_fFlyingSpeed;
        }

        protected virtual void OnDisable()
        {
            m_hRigid.velocity = Vector2.zero;
        }

        #endregion

        #region Helper

        #endregion
    }
}