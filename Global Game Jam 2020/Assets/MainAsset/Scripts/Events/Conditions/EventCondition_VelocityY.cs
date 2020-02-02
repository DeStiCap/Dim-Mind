using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Event;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "EventCondition_VelocityY", menuName = "DSC/Event/Condition/Velocity Y")]
    public class EventCondition_VelocityY : EventCondition
    {
        #region Enum

        protected enum VelocityYType
        {
            Zero    =   1 << 0,
            Up      =   1 << 1,
            Down    =   1 << 2
        }


        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [EnumMask]
        [SerializeField] protected VelocityYType m_eYType;
        [SerializeField] protected float m_fCheckY;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(EventConditionData hData)
        {
            var hPlayer = Global_GameplayManager.playerController;
            if (hPlayer == null || hPlayer.actorData.m_hRigid == null)
                return false;

            var vVel = hPlayer.actorData.m_hRigid.velocity;

            if (FlagUtility.HasFlagUnsafe(m_eYType, VelocityYType.Zero) && vVel.y == m_fCheckY)
                return true;

            if (FlagUtility.HasFlagUnsafe(m_eYType, VelocityYType.Up) && vVel.y > m_fCheckY)
                return true;

            if (FlagUtility.HasFlagUnsafe(m_eYType, VelocityYType.Down) && vVel.y < m_fCheckY)
                return true;
                

            return false;
        }

        #endregion
    }
}