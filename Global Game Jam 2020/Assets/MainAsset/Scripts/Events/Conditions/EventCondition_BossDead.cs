using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Event;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "EventCondition_BossDead", menuName = "DSC/Event/Condition/Boss Dead")]
    public class EventCondition_BossDead : EventCondition
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected bool m_bIsTrue;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(EventConditionData hData)
        {
            return (m_bIsTrue == Global_GameplayManager.bossDead);
        }

        #endregion
    }
}