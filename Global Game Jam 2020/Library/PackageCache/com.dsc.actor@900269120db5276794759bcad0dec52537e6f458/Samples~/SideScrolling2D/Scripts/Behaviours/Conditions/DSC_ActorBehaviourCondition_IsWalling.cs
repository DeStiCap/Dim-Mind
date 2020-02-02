using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "Behaviour_IsWalling", menuName = "DSC/Actor/Behaviour/Condition/Is Walling")]
    public class DSC_ActorBehaviourCondition_IsWalling : DSC_ActorBehaviourCondition
    {
        #region Variable -Inspector
#pragma warning disable 0649

        [SerializeField] protected bool m_bIsTrue = true;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (!PassAllCondition(hActorData, lstBehaviourData))
                return false;

            return (m_bIsTrue == FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.IsWalling));
        }

        #endregion
    }
}