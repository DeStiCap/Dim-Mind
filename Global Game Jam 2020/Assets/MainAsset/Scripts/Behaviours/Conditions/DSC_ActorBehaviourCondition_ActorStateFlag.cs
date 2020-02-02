﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "BehaviourCondition_ActorStateFlag", menuName = "DSC/Actor/Behaviour/Condition/Actor State Flag")]
    public class DSC_ActorBehaviourCondition_ActorStateFlag : DSC_ActorBehaviourCondition
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected ActorStateFlag m_eFlag;
        [SerializeField] protected bool m_bHasFlag;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Override

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            return (m_bHasFlag == FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, m_eFlag));
        }

        #endregion

        #region Helper

        #endregion
    }
}