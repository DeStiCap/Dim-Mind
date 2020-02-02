using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_PlayAnimation", menuName = "DSC/Actor/Behaviour/Play Animation")]
    public class DSC_ActorBehaviour_PlayAnimation : DSC_ActorBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Override

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (hActorData.m_hAnimation == null || hActorData.m_hRigid == null)
                return;

            var hAnim = hActorData.m_hAnimation.m_hSpineAnimationState;
            var vVelocity = hActorData.m_hRigid.velocity;

            if(hActorData.m_eCurrentAnimation == ActorAnimation.Airing)
            {
                if (vVelocity.y <= 0 && FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.IsGrounding) && !FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag,ActorStateFlag.HoldingJump))
                {
                    hAnim.SetAnimation(0, hActorData.m_hAnimation.m_sLandingAnimation, false);
                    hAnim.AddAnimation(0, hActorData.m_hAnimation.m_sIdleAnimation, true,0.2f);
                    hActorData.m_eCurrentAnimation = ActorAnimation.Idle;
                }
            }
            else if (FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.Walking))
            {
                if (hActorData.m_eCurrentAnimation != ActorAnimation.Running)
                {
                    hAnim.SetAnimation(0, hActorData.m_hAnimation.m_sRunAnimationName, true);
                    hActorData.m_eCurrentAnimation = ActorAnimation.Running;
                }
            }
            else
            {
                if (hActorData.m_eCurrentAnimation != ActorAnimation.Idle)
                {
                    hAnim.SetAnimation(0, hActorData.m_hAnimation.m_sIdleAnimation, true);
                    hActorData.m_eCurrentAnimation = ActorAnimation.Idle;
                }
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}