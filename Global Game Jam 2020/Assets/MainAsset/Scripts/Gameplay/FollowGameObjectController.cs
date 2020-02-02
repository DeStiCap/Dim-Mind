using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class FollowGameObjectController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Transform m_hFollowTarget;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        protected Vector2 m_vOffset;

        #endregion

        #region Base - Mono

        private void Awake()
        {
            m_vOffset = transform.position - m_hFollowTarget.position;
        }

        protected virtual void Update()
        {
            if (m_hFollowTarget)
            {
                Vector3 vPos = m_hFollowTarget.position + (Vector3)m_vOffset;
                vPos.z = 0;

                transform.position = vPos;
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}