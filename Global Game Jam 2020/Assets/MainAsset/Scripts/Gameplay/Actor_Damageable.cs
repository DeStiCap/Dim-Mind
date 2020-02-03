using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [RequireComponent(typeof(DSC_ActorController))]
    public class Actor_Damageable : MonoBehaviour, IDamageable
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Vector2 m_vKnockbackForce;
        [Min(0)]
        [SerializeField] protected float m_fIFrameAfterDamageDuration;

        [SerializeField] protected AudioClip m_hTakeDamageSound;

        [Header("Events")]
        [SerializeField] protected UnityEvent m_hDeadEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        protected DSC_ActorController m_hActorController;

        protected float m_fLastDamageTime;

        protected float m_fFreezeDuration;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hActorController = GetComponent<DSC_ActorController>();
        }

        protected virtual void Update()
        {
            if (FlagUtility.HasFlagUnsafe(m_hActorController.actorData.m_eStateFlag, ActorStateFlag.IsDamage))
            {
                if(Time.time >= m_fLastDamageTime + m_fIFrameAfterDamageDuration)
                {
                    m_hActorController.actorData.m_eStateFlag &= ~ActorStateFlag.IsDamage;
                }
            }

            if (FlagUtility.HasFlagUnsafe(m_hActorController.actorData.m_eStateFlag, ActorStateFlag.Freezing))
            {
                if(Time.time >= m_fLastDamageTime + m_fFreezeDuration)
                {
                    m_hActorController.actorData.m_eStateFlag &= ~ActorStateFlag.Freezing;
                }
            }
        }

        #endregion

        #region Interface

        public void DoDamage(int nDamage)
        {

            var hStat = m_hActorController.actorData.m_hStatus.status;
            hStat.m_nHP -= nDamage;
            m_hActorController.actorData.m_hStatus.status = hStat;

            m_hActorController.takeDamageEvent?.Invoke();

            if (hStat.m_nHP <= 0)
            {
                m_hDeadEvent?.Invoke();

                gameObject.SetActive(false);

                m_hActorController.deadEvent?.Invoke();

                if (m_hActorController.actorData.m_hActor.CompareTag(TagUtility.Name.player) && !Global_GameplayManager.winGame)
                {
                    Global_GameplayManager.PlayerDead();
                }

                return;
            }

            if(m_hActorController.actorData.m_hAnimation != null && m_hActorController.actorData.m_hAnimation.m_bPlayer)
            {
                var hAnim = m_hActorController.actorData.m_hAnimation.m_hSpineAnimationState;
                hAnim.SetAnimation(0,
                    m_hActorController.actorData.m_hAnimation.m_sDamageAnimation, false);

                hAnim.SetEmptyAnimation(1,0);

                m_hActorController.actorData.m_eCurrentAnimation = ActorAnimation.Hurting;
            }

            if(m_hActorController.actorData.m_hAudio && m_hTakeDamageSound)
            {
                m_hActorController.actorData.m_hAudio.PlayOneShot(m_hTakeDamageSound);
            }

            m_hActorController.actorData.m_eStateFlag |= ActorStateFlag.IsDamage;

            m_hActorController.actorData.m_eStateFlag &= ~ActorStateFlag.GroundPoundCasting;
            m_hActorController.actorData.m_eStateFlag &= ~ActorStateFlag.GroundPounding;

            m_fLastDamageTime = Time.time;            
        }

        public void DoDamage(int nDamage, GameObject hAttacker)
        {
            if (FlagUtility.HasFlagUnsafe(m_hActorController.actorData.m_eStateFlag, ActorStateFlag.IsDamage))
                return;

            DoDamage(nDamage);
        }

        public void DoDamage(int nDamage, Vector3 vPosition)
        {
            if (FlagUtility.HasFlagUnsafe(m_hActorController.actorData.m_eStateFlag, ActorStateFlag.IsDamage))
                return;

            if (FlagUtility.HasFlagUnsafe(m_hActorController.actorData.m_eStateFlag, ActorStateFlag.GroundPounding))
            {
                Vector2 vDifferent = m_hActorController.actorData.m_hActor.position - vPosition;
                if(vDifferent.y >= vDifferent.x)
                {
                    Debug.Log("Pounding Invisible.");
                    return;
                }
            }

            if (m_hActorController.actorData.m_hRigid)
            {
                m_hActorController.actorData.m_hRigid.velocity = Vector2.zero;

                var vForce = m_vKnockbackForce;

                if (transform.position.x > vPosition.x)
                {
                    if (FlagUtility.HasFlagUnsafe(m_hActorController.actorData.m_eStateFlag, ActorStateFlag.FacingRight))
                    {
                        m_hActorController.Flip();
                    }

                    vForce.x = -vForce.x;
                }
                else if (!FlagUtility.HasFlagUnsafe(m_hActorController.actorData.m_eStateFlag, ActorStateFlag.FacingRight))
                {
                    m_hActorController.Flip();
                }

                m_hActorController.actorData.m_hRigid.AddForce(vForce);
            }

            DoDamage(nDamage);
        }

        public void DoDamage(int nDamage, float fFreezeDuration)
        {
            
            DoDamage(nDamage);
            m_hActorController.actorData.m_eStateFlag |= ActorStateFlag.Freezing;
            m_hActorController.actorData.m_eStateFlag &= ~ActorStateFlag.IsDamage;
            m_fFreezeDuration = fFreezeDuration;

            if (m_hActorController.actorData.m_hRigid)
            {
                m_hActorController.actorData.m_hRigid.velocity = Vector2.zero;
            }
        }

        #endregion

        #region Helper

        #endregion

    }
}