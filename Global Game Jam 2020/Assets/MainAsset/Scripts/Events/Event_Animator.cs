using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class Event_Animator : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] Animator[] m_arrAnim;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        #endregion


        #region Events

        public void PlayAnimation()
        {
            if (m_arrAnim == null || m_arrAnim.Length <= 0)
                return;

            for (int i = 0; i < m_arrAnim.Length; i++)
            {
                var hAnim = m_arrAnim[i];
                if (hAnim != null)
                    hAnim.speed = 1;
            }
        }

        public void StopAnimation()
        {
            if (m_arrAnim == null || m_arrAnim.Length <= 0)
                return;

            for(int i = 0; i < m_arrAnim.Length; i++)
            {
                var hAnim = m_arrAnim[i];
                if (hAnim != null)
                    hAnim.speed = 0;
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}