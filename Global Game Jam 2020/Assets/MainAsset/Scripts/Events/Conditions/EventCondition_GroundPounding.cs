using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Event;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "EventCondition_GroundPounding", menuName = "DSC/Event/Condition/Ground Pounding")]
    public class EventCondition_GroundPounding : EventCondition
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected bool isTrue;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(EventConditionData hData)
        {
            var hPlayer = Global_GameplayManager.playerController;
            if (hPlayer == null)
                return false;

            return (isTrue == FlagUtility.HasFlagUnsafe(hPlayer.actorData.m_eStateFlag,ActorStateFlag.GroundPounding));
        }

        #endregion
    }
}