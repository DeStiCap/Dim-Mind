using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "BehaviourCondition_WinGame", menuName = "DSC/Actor/Behaviour/Condition/Win Game")]
    public class DSC_ActorBehaviourCondition_WinGame : DSC_ActorBehaviourCondition
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected bool m_bTrue;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Override

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            return (m_bTrue == Global_GameplayManager.winGame);
        }

        #endregion

        #region Helper

        #endregion
    }
}