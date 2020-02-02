using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_FlyingAttack", menuName = "DSC/Actor/Behaviour/Flying Attack")]
    public class DSC_ActorBehaviour_FlyingAttack : DSC_ActorBehaviour
    {
        #region Data

        public struct AttackCacheData : IActorBehaviourData
        {
            public bool m_bAttacking;
            public Vector2 m_vAttackStartPos;
            public Vector2 m_vAttackDestination;
            public Vector2 m_vControlPoint;
            public float m_fAttackStartTime;
            public float m_fAttackDuration;
            public bool m_bFlyAgain;
            public float m_fLastAttackTime;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Min(0)]
        [SerializeField] protected float m_fAttackDelay;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new AttackCacheData
            {
                
            });
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            lstBehaviourData.Remove<AttackCacheData>();

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.Fighting))
                return;

            if (!lstBehaviourData.TryGetData(out AttackCacheData hData,out int nIndex))
                return;

            if (!hData.m_bAttacking)
                StartAttack(hActorData, lstBehaviourData, hData, nIndex);
            else
                ProcessAttack(hActorData, lstBehaviourData, hData, nIndex);
        }

        #endregion

        #region Main

        protected virtual void StartAttack(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData,AttackCacheData hData,int nIndex)
        {
            var hPlayer = Global_GameplayManager.playerController;
            if (hPlayer == null)
                return;

            hData.m_vAttackStartPos = hActorData.m_hActor.position;
            hData.m_vAttackDestination = hPlayer.actorData.m_hActor.position;
            hData.m_vControlPoint = hData.m_vAttackStartPos + (hData.m_vAttackDestination - hData.m_vAttackStartPos) / 2 + Vector2.down * 2.0f;

            hData.m_fAttackStartTime = Time.time;
            hData.m_fAttackDuration = (Vector2.Distance(hData.m_vAttackStartPos, hData.m_vAttackDestination) / (hActorData.m_hStatus.status.m_fMoveSpeed * 2));

            hData.m_bAttacking = true;
            lstBehaviourData[nIndex] = hData;
        }

        protected virtual void ProcessAttack(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData,AttackCacheData hData,int nIndex)
        {
            float fTime = (Time.time - hData.m_fAttackStartTime) / hData.m_fAttackDuration;
            Vector2 vPos1 = Vector2.Lerp(hData.m_vAttackStartPos, hData.m_vControlPoint, fTime);
            Vector2 vPos2 = Vector2.Lerp(hData.m_vControlPoint, hData.m_vAttackDestination, fTime);
            hActorData.m_hActor.position = Vector2.Lerp(vPos1, vPos2, fTime);

            if(Time.time >= hData.m_fAttackStartTime + hData.m_fAttackDuration)
            {
                if (hData.m_bFlyAgain)
                    hData.m_bAttacking = false;
                else
                {
                    hData.m_fAttackStartTime = Time.time;
                    hData.m_vAttackDestination.y = hData.m_vAttackStartPos.y + 1;
                    float fX = (hData.m_vAttackDestination.x - hData.m_vAttackStartPos.x);
                    hData.m_vAttackDestination.x += fX;

                    hData.m_vAttackStartPos = hActorData.m_hActor.position;
                    hData.m_vControlPoint = hData.m_vAttackStartPos + (hData.m_vAttackDestination - hData.m_vAttackStartPos) / 2 + Vector2.up * 2.0f;
                    hData.m_fAttackDuration = (Vector2.Distance(hData.m_vAttackStartPos, hData.m_vAttackDestination) / (hActorData.m_hStatus.status.m_fMoveSpeed));

                    hData.m_bFlyAgain = true;
                }
            }

            lstBehaviourData[nIndex] = hData;
        }

        

        #endregion

        #region Helper

        #endregion
    }
}