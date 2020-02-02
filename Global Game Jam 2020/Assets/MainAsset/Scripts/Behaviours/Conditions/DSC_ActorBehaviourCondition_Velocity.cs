using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "BehaviourCondition_Velocity", menuName = "DSC/Actor/Behaviour/Condition/Velocity")]
    public class DSC_ActorBehaviourCondition_Velocity : DSC_ActorBehaviourCondition
    {
        #region Enum

        [System.Flags]
        protected enum VelocityType
        {
            ZeroX   =   1 << 0,
            ZeroY   =   1 << 1,
            Left    =   1 << 2,
            Right   =   1 << 3,
            Up      =   1 << 4,
            Down    =   1 << 5
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Velocity")]
        [SerializeField] protected VelocityType m_eVelocity;
   
#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (!PassAllCondition(hActorData, lstBehaviourData))
                return false;

            if (hActorData.m_hRigid == null)
                return true;

            Vector2 vVelocity = hActorData.m_hRigid.velocity;
            return (CheckVelocityX(vVelocity) && CheckVelocityY(vVelocity));
        }

        #endregion

        #region Main

        protected virtual bool CheckVelocityX(Vector2 vVelocity)
        {
            if (FlagUtility.HasFlagUnsafe(m_eVelocity, VelocityType.ZeroX) && vVelocity.x == 0)
                return true;

            if (FlagUtility.HasFlagUnsafe(m_eVelocity, VelocityType.Left) && vVelocity.x < 0)
                return true;

            if (FlagUtility.HasFlagUnsafe(m_eVelocity, VelocityType.Right) && vVelocity.x > 0)
                return true;

            return !FlagUtility.HasFlagUnsafe(m_eVelocity,(VelocityType.ZeroX | VelocityType.Left | VelocityType.Right));
        }

        protected virtual bool CheckVelocityY(Vector2 vVelocity)
        {
            if (FlagUtility.HasFlagUnsafe(m_eVelocity, VelocityType.ZeroY) && vVelocity.y == 0)
                return true;

            if (FlagUtility.HasFlagUnsafe(m_eVelocity, VelocityType.Up) && vVelocity.y > 0)
                return true;

            if (FlagUtility.HasFlagUnsafe(m_eVelocity, VelocityType.Down) && vVelocity.y < 0)
                return true;

            return (!FlagUtility.HasFlagUnsafe(m_eVelocity,VelocityType.ZeroY | VelocityType.Up | VelocityType.Down));
        }

        #endregion
    }
}