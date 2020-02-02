using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_WallJump", menuName = "DSC/Actor/Behaviour/Wall Jump")]
    public class DSC_ActorBehaviour_WallJump : DSC_ActorBehaviour
    {
        #region Data

        public struct WallJumpCacheData : IActorBehaviourData
        {
            public UnityAction<ActorData, List<IActorBehaviourData>> m_actJump;
            public float m_fWallJumpLastTime;
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Vector2 m_vJumpForce;
        [Min(0)]
        [SerializeField] protected float m_fWallJumpDuration;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new WallJumpCacheData
            {
                m_actJump = OnWallJump,
                m_fWallJumpLastTime = -100,
            });
        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnStartBehaviour(hActorData, lstBehaviourData);

            if (lstBehaviourData.TryGetData(out WallJumpCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Add((InputEventType.Jump, GetInputType.Down), hOutData.m_actJump);
            }
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData(out WallJumpCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Remove((InputEventType.Jump, GetInputType.Down), hOutData.m_actJump);
            }

            base.OnStopBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData<WallJumpCacheData>(out int nOutIndex))
            {
                lstBehaviourData.RemoveAt(nOutIndex);
            }

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.WallJumping))
                return;

            if (!lstBehaviourData.TryGetData(out WallJumpCacheData hData))
                return;

            if(Time.time >= hData.m_fWallJumpLastTime + m_fWallJumpDuration)
            {
                hActorData.m_eStateFlag &= ~ActorStateFlag.WallJumping;
            }
        }

        #endregion

        #region Main

        protected virtual void OnWallJump(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (hActorData.m_hRigid == null || hActorData.m_hStatus == null 
                || hActorData.m_hInputData.m_fHorizontal == 0 || !FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag,ActorStateFlag.IsWalling)
                || FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag,ActorStateFlag.WallJumping) || !lstBehaviourData.TryGetData(out WallJumpCacheData hData,out var nIndex))
                return;


            Vector2 vForce = m_vJumpForce;
            if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight))
                vForce.x = -vForce.x;

            hActorData.m_hRigid.velocity = Vector2.zero;

            hActorData.m_hRigid.AddForce(vForce);

            hActorData.m_eStateFlag |= ActorStateFlag.HoldingJump;
            hActorData.m_eStateFlag |= ActorStateFlag.WallJumping;
            hData.m_fWallJumpLastTime = Time.time;

            lstBehaviourData[nIndex] = hData;

            if (hActorData.m_hAnimation)
            {
                hActorData.m_hAnimation.m_hSpineAnimationState.SetAnimation(0, hActorData.m_hAnimation.m_sClimbAnimation, false);
                hActorData.m_hAnimation.m_hSpineAnimationState.AddAnimation(0, hActorData.m_hAnimation.m_sJumpingAnimation, true, 0.3f);
            }
        }

        #endregion
    }
}