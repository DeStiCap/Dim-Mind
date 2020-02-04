using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class Event_GroundPound : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_ActorController m_hUser;
        [Min(0)]
        [SerializeField] protected float m_fBoundForce;
        [Min(0)]
        [SerializeField] protected float m_fDamageMultiplier = 1;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        #endregion

        #region Events

        public void GroundPoundBound()
        {
            if (m_hUser == null || m_hUser.actorData.m_hRigid == null)
                return;

            

            var hRigid = m_hUser.actorData.m_hRigid;

            var vVel = hRigid.velocity;
            vVel.y = 0;
            hRigid.velocity = vVel;

            
            hRigid.AddForce(new Vector2(0, m_fBoundForce));
            if (m_hUser.actorData.m_hAnimation)
            {
                m_hUser.actorData.m_hAnimation.m_hSpineAnimationState.SetAnimation(0,
                    m_hUser.actorData.m_hAnimation.m_sJumpingAnimation, true);
            }


            m_hUser.actorData.m_eStateFlag &= ~ActorStateFlag.GroundPounding;
        }

        public void GroundPoundBound(GameObject hHitTarget)
        {
            if (hHitTarget.gameObject.CompareTag(TagUtility.Name.destroyableGround)
                || hHitTarget.gameObject.CompareTag(TagUtility.Name.rock)
                || hHitTarget.gameObject.CompareTag(TagUtility.Name.enemy))
                GroundPoundBound();
            else if (m_hUser)
            {
                m_hUser.actorData.m_eStateFlag &= ~ActorStateFlag.GroundPoundCasting;
                m_hUser.actorData.m_eStateFlag &= ~ActorStateFlag.GroundPounding;
            }
        }

        public void GroundPoundDamage(GameObject hTarget)
        {
            if (hTarget == null)
                return;

            if (hTarget.CompareTag(TagUtility.Name.rock)|| hTarget.CompareTag(TagUtility.Name.destroyableGround))
            {
                var hDestroyable = hTarget.GetComponent<IDestroyable>();
                if(hDestroyable != null)
                {
                    hDestroyable.Destroy();
                }
                return;
            }

            if (m_hUser)
            {
                var hDamageable = hTarget.GetComponent<IDamageable>();
                if (hDamageable != null)
                {
                    hDamageable.DoDamage(Mathf.RoundToInt(m_hUser.actorData.m_hStatus.status.m_nAttack * m_fDamageMultiplier));
                }
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}