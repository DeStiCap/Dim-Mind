using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class Event_Damage : MonoBehaviour
    {
        #region Enum

        protected enum TargetTag
        {
            Player,
            Enemy
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected TargetTag m_eTargetTag;
        [Min(0)]
        [SerializeField] protected int m_nDamage;

        [Min(0)]
        [SerializeField] protected float m_fFreezingDuration;

        [Header("Events")]
        [SerializeField] protected UnityEvent m_hDoDamageEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public int damage
        {
            get { return m_nDamage; }
            set
            {
                m_nDamage = value;
                if (m_nDamage < 1)
                    m_nDamage = 1;
            }
        }

        public float freezingDuration
        {
            get { return m_fFreezingDuration; }
            set
            {
                m_fFreezingDuration = value;
                if (m_fFreezingDuration < 0)
                    m_fFreezingDuration = 0;
            }
        }

        #endregion

        #endregion

        #region Base - Mono

        #endregion

        #region Events

        public void DoDamage(GameObject hTarget)
        {
            if (hTarget == null)
                return;

            switch (m_eTargetTag)
            {
                case TargetTag.Player:
                    if (hTarget.CompareTag(TagUtility.Name.player))
                        MainDoDamage(hTarget);
                    break;

                case TargetTag.Enemy:
                    if (hTarget.CompareTag(TagUtility.Name.enemy))
                        MainDoDamage(hTarget);
                    break;
            }
        }

        public void DoDamageFreezing(GameObject hTarget)
        {
            if (hTarget == null)
                return;

            switch (m_eTargetTag)
            {
                case TargetTag.Player:
                    if (hTarget.CompareTag(TagUtility.Name.player))
                        MainDoDamage(hTarget,true);
                    break;

                case TargetTag.Enemy:
                    if (hTarget.CompareTag(TagUtility.Name.enemy))
                        MainDoDamage(hTarget,true);
                    break;
            }
        }

        #endregion

        #region Main

        protected virtual void MainDoDamage(GameObject hTarget, bool bFreezing = false)
        {
            var hDamageable = hTarget.GetComponent<IDamageable>();
            if(hDamageable != null)
            {
                if (!bFreezing)
                    hDamageable.DoDamage(m_nDamage, transform.position);
                else
                    hDamageable.DoDamage(m_nDamage, m_fFreezingDuration);

                m_hDoDamageEvent?.Invoke();
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}