using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_GroundPound", menuName = "DSC/Actor/Behaviour/Ground Pound")]
    public class DSC_ActorBehaviour_GroundPound : DSC_ActorBehaviour
    {
        #region Data

        public class GroundPoundCacheData : IActorBehaviourData
        {
            public UnityAction<ActorData, List<IActorBehaviourData>> m_actDownAttack;
            public float m_fGroundPoundStartTime;
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected float m_fStartForce;
        [Min(0)]
        [SerializeField] protected float m_fCastingTime;
        [Min(0)]
        [SerializeField] protected int m_nEventIndex;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new GroundPoundCacheData
            {
                m_actDownAttack = OnDownAttack
            });
        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnStartBehaviour(hActorData, lstBehaviourData);

            if (lstBehaviourData.TryGetDataRW(out GroundPoundCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Add((InputEventType.Attack, GetInputType.Down), hOutData.m_actDownAttack);
            }
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetDataRW(out GroundPoundCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Remove((InputEventType.Attack, GetInputType.Down), hOutData.m_actDownAttack);
            }

            base.OnStopBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            lstBehaviourData.RemoveRW<GroundPoundCacheData>();

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (!lstBehaviourData.TryGetDataRW(out GroundPoundCacheData hData))
                return;

            if (hActorData.m_hRigid)
            {
                if (FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.GroundPoundCasting))
                {
                    if(Time.time >= hData.m_fGroundPoundStartTime + m_fCastingTime)
                    {
                        hActorData.m_eStateFlag &= ~ActorStateFlag.GroundPoundCasting;
                        GroundPound(hActorData, lstBehaviourData);
                    }   
                }
            }
        }

        public override void OnFixedUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnFixedUpdateBehaviour(hActorData, lstBehaviourData);

            if (hActorData.m_hRigid)
            {
                if (FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.GroundPoundCasting))
                {
                    hActorData.m_hRigid.velocity = Vector2.zero;
                }
            }
        }

        #endregion

        #region Main

        protected virtual void OnDownAttack(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (hActorData.m_hInputData.m_fVertical >= 0 || hActorData.m_hRigid == null || hActorData.m_hStatus == null 
                || FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag,ActorStateFlag.GroundPoundCasting)
                || FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag,ActorStateFlag.GroundPounding)
                || !lstBehaviourData.TryGetDataRW(out GroundPoundCacheData hData))
                return;


            hActorData.m_eStateFlag |= ActorStateFlag.GroundPoundCasting;
            hActorData.m_hRigid.velocity = Vector2.zero;
            hData.m_fGroundPoundStartTime = Time.time;

            if (hActorData.m_hAnimation)
            {
                hActorData.m_hAnimation.m_hSpineAnimationState.SetAnimation(0,
                    hActorData.m_hAnimation.m_sGroundPoundCastingAnimation, false);
            }
        }

        protected virtual void GroundPound(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            
            hActorData.m_eStateFlag |= ActorStateFlag.GroundPounding;

            var vVel = hActorData.m_hRigid.velocity;
            if (vVel.y > 0)
            {
                vVel.y = 0;
                hActorData.m_hRigid.velocity = vVel;
            }

            hActorData.m_hRigid.AddForce(new Vector2(0, -m_fStartForce));

            if (lstBehaviourData.TryGetData(out ActorMonoData<Actor_EventController> hEventController))
            {
                hEventController.m_hMono.RunEvent(m_nEventIndex);
            }

            if (hActorData.m_hAnimation)
            {
                hActorData.m_hAnimation.m_hSpineAnimationState.SetAnimation(0,
                    hActorData.m_hAnimation.m_sGroundPoundAnimation, false);
            }
        }

        #endregion
    }
}