using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class MarkPointController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Transform[] m_arrMarkPoint;

        [Header("Option")]
        [SerializeField] protected bool m_bSetRootOnAwake;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public Transform[] markPointArray { get { return m_arrMarkPoint; } }

        #endregion

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            if (m_bSetRootOnAwake)
                transform.SetParent(null);
        }

        #endregion

        #region Helper

        #endregion
    }
}