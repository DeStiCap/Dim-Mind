using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;
using DSC.Core;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_FlipFacing", menuName = "DSC/Actor/Behaviour/Flip Facing")]
    public class DSC_ActorBehaviour_FlipFacing : DSC_ActorBehaviour
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected float m_fRightAngle = 0;
        [SerializeField] protected float m_fLeftAngle = 180;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);
                       
            if ((hActorData.m_hInputData.m_fHorizontal > 0 && !FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight))
                || (hActorData.m_hInputData.m_fHorizontal < 0 && FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight)))
            {
                var hController = (DSC_ActorController)hActorData.m_hController;
                hController.Flip();
            }
        }

        #endregion
    }
}