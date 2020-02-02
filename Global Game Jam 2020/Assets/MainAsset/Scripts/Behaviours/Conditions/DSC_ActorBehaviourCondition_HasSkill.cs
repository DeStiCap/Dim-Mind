using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "BehaviourCondition_HasSkill", menuName = "DSC/Actor/Behaviour/Condition/Has Skill")]
    public class DSC_ActorBehaviourCondition_HasSkill : DSC_ActorBehaviourCondition
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Skill m_eSkill;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Override

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            return FlagUtility.HasFlagUnsafe(hActorData.m_eSkill, m_eSkill);
        }

        #endregion

        #region Helper

        #endregion
    }
}