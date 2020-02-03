using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Event;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "EventCondition_NeverInput", menuName = "DSC/Event/Condition/Never Input")]
    public class EventCondition_NeverInput : EventCondition
    {
        #region Variable - Inspector
#pragma warning disable 0649

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(EventConditionData hData)
        {
            var hPlayer = Global_GameplayManager.playerController;
            if (hPlayer)
            {
                return !hPlayer.actorData.m_hInputData.m_bPressInput;
            }

            return true;
        }

        #endregion
    }
}