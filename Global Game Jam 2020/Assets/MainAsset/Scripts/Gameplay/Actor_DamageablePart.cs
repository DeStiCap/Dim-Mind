using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    public class Actor_DamageablePart : MonoBehaviour, IDamageable
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Actor_Damageable m_hMainDamageable;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        protected Actor_Damageable mainDamageable
        {
            get
            {
                if (m_hMainDamageable == null)
                    Debug.LogWarning("Don't have main damageable set.",gameObject);

                return m_hMainDamageable;
            }
        }

        #endregion

        #endregion

        #region Base - Mono

        #endregion

        #region Interface

        public void DoDamage(int nDamage)
        {
            mainDamageable?.DoDamage(nDamage);
        }

        public void DoDamage(int nDamage, GameObject hAttacker)
        {
            mainDamageable?.DoDamage(nDamage, hAttacker);
        }

        public void DoDamage(int nDamage, Vector3 vPosition)
        {
            mainDamageable?.DoDamage(nDamage, vPosition);
        }

        public void DoDamage(int nDamage, float fFreezeDuration)
        {
            mainDamageable?.DoDamage(nDamage, fFreezeDuration);
        }

        #endregion

        #region Helper

        #endregion
    }
}