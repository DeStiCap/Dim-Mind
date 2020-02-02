using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_SearchPlayer", menuName = "DSC/Actor/Behaviour/Search Player")]
    public class DSC_ActorBehaviour_SearchPlayer : DSC_ActorBehaviour
    {
        #region Enum

        [System.Flags]
        protected enum SearchType
        {
            X       =   1 << 0,
            Y       =   1 << 1,
            Angle   =   1 << 2
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected SearchType m_eSearhType;
        [Min(0)]
        [SerializeField] protected float m_fSightXRange;
        [Min(0)]
        [SerializeField] protected float m_fSightYRange;
        [Min(0)]
        [SerializeField] protected float m_fSightAngle;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Override

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            var hPlayer = Global_GameplayManager.playerController;
            if (hPlayer == null)
                return;

            Vector2 vPlayerPos = hPlayer.actorData.m_hActor.position;
            Vector2 vOwnPos = hActorData.m_hActor.position;

            if (FlagUtility.HasFlagUnsafe(m_eSearhType, SearchType.X))
            {

                if (vPlayerPos.x > vOwnPos.x && !FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight)
                    || vPlayerPos.x < vOwnPos.x && FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight))
                    goto NotFound;

                float fDistanceX = Mathf.Abs(vPlayerPos.x - vOwnPos.x);
                if (fDistanceX > m_fSightXRange)
                    goto NotFound;
            }

            if (FlagUtility.HasFlagUnsafe(m_eSearhType, SearchType.Y))
            {
                float fDistanceY = Mathf.Abs(vPlayerPos.y - vOwnPos.y);
                if (fDistanceY > m_fSightYRange)
                    goto NotFound;
            }

            if (FlagUtility.HasFlagUnsafe(m_eSearhType, SearchType.Angle))
            {
                if (Vector2.Angle(vOwnPos, vPlayerPos) > m_fSightAngle)                
                    goto NotFound;
            }

            FoundPlayer(hActorData, lstBehaviourData);
            return;

        NotFound:
            NotFoundPlayer(hActorData, lstBehaviourData);
            ;
        }

        #endregion

        #region Main

        protected virtual void FoundPlayer(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            hActorData.m_eStateFlag |= ActorStateFlag.Fighting;
        }

        protected virtual void NotFoundPlayer(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            hActorData.m_eStateFlag &= ~ActorStateFlag.Fighting;
        }

        #endregion

        #region Helper

        #endregion
    }
}