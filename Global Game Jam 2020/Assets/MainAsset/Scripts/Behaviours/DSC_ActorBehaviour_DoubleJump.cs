using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_DoubleJump", menuName = "DSC/Actor/Behaviour/Double Jump")]
    public class DSC_ActorBehaviour_DoubleJump : DSC_ActorBehaviour
    {
        #region Data

        public struct DoubleJumpCacheData : IActorBehaviourData
        {
            public UnityAction<ActorData, List<IActorBehaviourData>> m_actJump;
        }

        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new DoubleJumpCacheData
            {
                m_actJump = OnJump
            });
        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnStartBehaviour(hActorData, lstBehaviourData);

            if (lstBehaviourData.TryGetData(out DoubleJumpCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Add((InputEventType.Jump, GetInputType.Down), hOutData.m_actJump);
            }
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData(out DoubleJumpCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Remove((InputEventType.Jump, GetInputType.Down), hOutData.m_actJump);
            }

            base.OnStopBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            lstBehaviourData.Remove<DoubleJumpCacheData>();

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        #endregion

        #region Main

        protected virtual void OnJump(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (hActorData.m_hRigid == null || hActorData.m_hStatus == null 
                || !FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag,ActorStateFlag.CanDoubleJump)
                || FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag,ActorStateFlag.WallJumping))
                return;

            var vVel = hActorData.m_hRigid.velocity;
            vVel.y = 0;
            hActorData.m_hRigid.velocity = vVel;
            hActorData.m_hRigid.AddForce(new Vector2(0, hActorData.m_hStatus.status.m_fDoubleJumpForce));

            hActorData.m_fJumpStartTime = Time.time;

            hActorData.m_eStateFlag |= ActorStateFlag.Jumping;
            hActorData.m_eStateFlag |= ActorStateFlag.HoldingJump;
            hActorData.m_eStateFlag &= ~ActorStateFlag.CanDoubleJump;

            if (hActorData.m_hAnimation != null)
            {
                hActorData.m_hAnimation.m_hSpineAnimationState.SetAnimation(0, hActorData.m_hAnimation.m_sJumpStartAnimation, false);
                hActorData.m_hAnimation.m_hSpineAnimationState.AddAnimation(0, hActorData.m_hAnimation.m_sJumpingAnimation, true, 0.2f);
                hActorData.m_eCurrentAnimation = ActorAnimation.Airing;
            }
        }

        #endregion
    }
}