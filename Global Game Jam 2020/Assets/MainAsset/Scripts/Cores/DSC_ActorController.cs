using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    #region Data

    public struct ActorMonoData<Mono> : IActorBehaviourData where Mono : MonoBehaviour
    {
        public Mono m_hMono;
    }

    #endregion

    public class DSC_ActorController : BaseActorController<ActorData,DSC_ActorBehaviour>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected List<DSC_ActorBehaviour> m_lstBehaviour;
        [SerializeField] protected bool m_bDefaultRightFacing;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override ActorData actorData { get { return m_hData; } }

        protected override List<DSC_ActorBehaviour> listBehaviour { get { return m_lstBehaviour; } set { m_lstBehaviour = value; } }

        public override List<IActorBehaviourData> listBehaviourData { get { return m_lstBehaviourData; } protected set { m_lstBehaviourData = value; } }

        [HideInInspector]
        public UnityEvent takeDamageEvent;
        
        [HideInInspector]
        public UnityEvent deadEvent;

        #endregion

        protected ActorData m_hData;

        protected List<IActorBehaviourData> m_lstBehaviourData = new List<IActorBehaviourData>();

        float m_fPreviousTimeScale = 1;
        Vector2 m_vPreviousVelocity;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            // Can change this to register in manager instead.
            m_hData = new ActorData(transform,this);

            if (m_bDefaultRightFacing)
            {
                m_hData.m_bDefaultRightFacing = true;
                m_hData.m_eStateFlag |= ActorStateFlag.FacingRight;
            }

            if (gameObject.CompareTag(TagUtility.Name.player))
            {
                Global_GameplayManager.playerController = this;
                m_hData.m_eSkill = Global_GameplayManager.playerSkill;
            }

            CreateAllBehaviour();
        }

        protected virtual void OnEnable()
        {
            if (gameObject.CompareTag(TagUtility.Name.player))
            {
                Global_GameplayManager.winGame = false;
                Global_GameplayManager.timeOut?.AddListener(OnTimeOut);
            }

            StartAllBehaviour();
        }

        protected virtual void OnDisable()
        {
            if (gameObject.CompareTag(TagUtility.Name.player))
            {
                Global_GameplayManager.timeOut?.RemoveListener(OnTimeOut);
            }

            StopAllBehaviour();
        }

        protected virtual void OnDestroy()
        {
            DestroyAllBehaviour();
        }

        protected virtual void Update()
        {
            float fTimeScale = Global_GameplayManager.myTimeScale;
            if(fTimeScale != m_fPreviousTimeScale)
            {
                m_fPreviousTimeScale = fTimeScale;

                if (fTimeScale == 0)
                {
                    StopAllBehaviour();

                    if (m_hData.m_hAnimation)
                    {
                        m_hData.m_hAnimation.m_hSpineAnimationState.TimeScale = 0;
                    }

                    if (m_hData.m_hRigid)
                    {
                        m_vPreviousVelocity = m_hData.m_hRigid.velocity;
                        m_hData.m_hRigid.velocity = Vector2.zero;
                        m_hData.m_hRigid.isKinematic = true;
                    }
                }
                else
                {
                    StartAllBehaviour();
                    if (m_hData.m_hAnimation)
                    {
                        
                        m_hData.m_hAnimation.m_hSpineAnimationState.TimeScale = 1;
                    }

                    if (m_hData.m_hRigid)
                    {
                        m_hData.m_hRigid.velocity = m_vPreviousVelocity;
                        m_hData.m_hRigid.isKinematic = false;
                    }
                }
            }

            if (fTimeScale == 0)
                return;
            

            ExecuteBehaviour();


            if (FlagUtility.HasFlagUnsafe(m_hData.m_eStateFlag, ActorStateFlag.Attacking))
            {
                if(Time.time >= m_hData.m_fLastAttackTime + 0.5f)
                {
                    m_hData.m_eStateFlag &= ~ActorStateFlag.Attacking;
                    if (m_hData.m_hAnimation)
                    {
                        m_hData.m_hAnimation.m_hSpineAnimationState.SetEmptyAnimation(1, 0);
                    }
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            if (Global_GameplayManager.myTimeScale == 0)
                return;

            FixedExecuteBehaviour();

            if (FlagUtility.HasFlagUnsafe(m_hData.m_eStateFlag,ActorStateFlag.Jumping) && m_hData.m_hRigid)
            {
                if(Time.time >= m_hData.m_fJumpStartTime + 0.05f && m_hData.m_hRigid.velocity.y <= 0)
                {
                    m_hData.m_eStateFlag &= ~ActorStateFlag.Jumping;
                }
            }
        }

        protected virtual void LateUpdate()
        {
            if (Global_GameplayManager.myTimeScale == 0)
                return;

            LateExecuteBehaviour();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            m_hData.m_hTriggerEventCallback?.Run(PhysicEventType.Enter, m_hData, m_lstBehaviourData, collision);
        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            m_hData.m_hTriggerEventCallback?.Run(PhysicEventType.Stay, m_hData, m_lstBehaviourData, collision);
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            m_hData.m_hTriggerEventCallback?.Run(PhysicEventType.Exit, m_hData, m_lstBehaviourData, collision);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            m_hData.m_hCollisionEventCallback?.Run(PhysicEventType.Enter, m_hData, m_lstBehaviourData, collision);
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            m_hData.m_hCollisionEventCallback?.Run(PhysicEventType.Stay, m_hData, m_lstBehaviourData, collision);
        }

        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            m_hData.m_hCollisionEventCallback?.Run(PhysicEventType.Exit, m_hData, m_lstBehaviourData, collision);
        }

        #endregion

        #region Events

        public virtual void SetIsGrounding(bool bGrounding)
        {
            if (bGrounding)
            {
                m_hData.m_eStateFlag |= ActorStateFlag.IsGrounding;
                m_hData.m_eStateFlag |= ActorStateFlag.CanDoubleJump;
            }
            else
                m_hData.m_eStateFlag &= ~ActorStateFlag.IsGrounding;
        }

        public virtual void SetIsWalling(bool bWalling)
        {
            if (bWalling)
                m_hData.m_eStateFlag |= ActorStateFlag.IsWalling;
            else
                m_hData.m_eStateFlag &= ~ActorStateFlag.IsWalling;
        }

        public virtual void Flip()
        {
            Quaternion qRightRot;
            Quaternion qLeftRot;
            if (m_bDefaultRightFacing)
            {
                qLeftRot = Quaternion.Euler(0, 180, 0);
                qRightRot = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                qLeftRot = Quaternion.Euler(0, 0, 0);
                qRightRot = Quaternion.Euler(0, 180, 0);
            }

            if (FlagUtility.HasFlagUnsafe(m_hData.m_eStateFlag, ActorStateFlag.FacingRight))
            {
                transform.localRotation = qLeftRot;
                m_hData.m_eStateFlag &= ~ActorStateFlag.FacingRight;
            }
            else
            {
                transform.localRotation = qRightRot;
                m_hData.m_eStateFlag |= ActorStateFlag.FacingRight;
            }
        }

        public void OnTimeOut()
        {
            if (!m_hData.m_hInputData.m_bPressInput)
            {
                Global_GameplayManager.winGame = true;
                Global_GameplayManager.winGameEvent?.Invoke();
                return;
            }

            var hDamageable = GetComponent<IDamageable>();
            if(hDamageable != null)
            {
                hDamageable.DoDamage(1000000);
            }
        }

        #endregion
    }
}