using System.Collections;
using System.Collections.Generic;
using DSC.Actor;
using DSC.Core;
using UnityEngine;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_Walk", menuName = "DSC/Actor/Behaviour/Walk")]
    public class DSC_ActorBehaviour_Walk : DSC_ActorBehaviour
    {
        #region Base - Override

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (hActorData.m_hRigid == null || hActorData.m_hStatus == null)
                return;

            float fHorizontal = hActorData.m_hInputData.m_fHorizontal;

            if (fHorizontal == 0)
            {
                if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.Walking))
                    return;

                hActorData.m_eStateFlag &= ~ActorStateFlag.Walking;
            }
            else if (FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.IsWalling))
                return;
            else if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.Walking))
                hActorData.m_eStateFlag |= ActorStateFlag.Walking;

            float fMoveSpeed = hActorData.m_hStatus.status.m_fMoveSpeed;
            Vector2 vVelocity = hActorData.m_hRigid.velocity;
            vVelocity.x = hActorData.m_hInputData.m_fHorizontal * fMoveSpeed;
            hActorData.m_hRigid.velocity = vVelocity;
        }

        #endregion
    }
}