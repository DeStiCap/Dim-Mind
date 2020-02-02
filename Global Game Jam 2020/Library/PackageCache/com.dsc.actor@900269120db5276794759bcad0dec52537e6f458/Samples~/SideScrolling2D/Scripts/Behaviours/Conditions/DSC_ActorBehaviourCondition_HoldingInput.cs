using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "BehaviourCondition_HoldingInput", menuName = "DSC/Actor/Behaviour/Condition/Holding Input")]
    public class DSC_ActorBehaviourCondition_HoldingInput : DSC_ActorBehaviourCondition
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected InputEventType m_eInput;
        [SerializeField] protected bool m_bIsHolding = true;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (!PassAllCondition(hActorData, lstBehaviourData))
                return false;

            return (m_bIsHolding == FlagUtility.HasFlagUnsafe(hActorData.m_hInputData.m_eHoldingInput,m_eInput));
        }

        #endregion
    }
}