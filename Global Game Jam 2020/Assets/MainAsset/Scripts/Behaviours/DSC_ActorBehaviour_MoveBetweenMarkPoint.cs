using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_MoveBetweenMarkPoint", menuName = "DSC/Actor/Behaviour/Move between mark point")]
    public class DSC_ActorBehaviour_MoveBetweenMarkPoint : DSC_ActorBehaviour
    {
        #region Enum

        public enum MoveWayType
        {
            Forward,
            Backward
        }

        #endregion

        #region Data

        public struct MoveCacheData : IActorBehaviourData
        {
            public MoveWayType m_eMoveWay;
            public int m_nNextPointIndex;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Min(0)]
        [SerializeField] protected float m_fMinDistance = 0.2f;
        [SerializeField] protected bool m_bOneWay;
        [SerializeField] protected bool m_bOnlyAxisX;
        [SerializeField] protected bool m_bFlipFacing = true;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Main

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new MoveCacheData
            {
                m_nNextPointIndex = -2
            });
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            lstBehaviourData.Remove<MoveCacheData>();

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            if (!lstBehaviourData.TryGetData(out ActorMonoData<Actor_MarkPointController> hActorMarkPoint)
                || !lstBehaviourData.TryGetData(out MoveCacheData hData, out int nIndex))
                return;

            var hMarkPointController = hActorMarkPoint.m_hMono.markPointController;
            if (hMarkPointController == null)
                return;

            var arrMarkPoint = hMarkPointController.markPointArray;
            if (arrMarkPoint == null || arrMarkPoint.Length <= 0)
                return;

            if(arrMarkPoint.Length < 2)
            {
                Debug.LogWarning("Can't move between because not enough data array.");
                return;
            }

            if(hData.m_nNextPointIndex < -1)
            {
                hActorData.m_hActor.transform.position = arrMarkPoint[0].position;
                hData.m_nNextPointIndex = 1;
            }

            Vector3 vNextPosition = Vector3.zero;

        CheckWay:
            ;

            switch (hData.m_eMoveWay)
            {
                case MoveWayType.Forward:
                    if(hData.m_nNextPointIndex >= arrMarkPoint.Length)
                    {
                        if (m_bOneWay)
                        {
                            hData.m_nNextPointIndex = 0;
                            goto CheckWay;
                        }

                        hData.m_eMoveWay = MoveWayType.Backward;
                        hData.m_nNextPointIndex = arrMarkPoint.Length - 2;
                        goto CheckWay;
                    }

                    vNextPosition = arrMarkPoint[hData.m_nNextPointIndex].position;
                    break;

                case MoveWayType.Backward:
                    if(hData.m_nNextPointIndex < 0)
                    {
                        hData.m_eMoveWay = MoveWayType.Forward;
                        hData.m_nNextPointIndex = 1;
                        goto CheckWay;
                    }

                    vNextPosition = arrMarkPoint[hData.m_nNextPointIndex].position;
                    break;
            }

            Vector2 vDifferent = vNextPosition - hActorData.m_hActor.position;
            if (m_bOnlyAxisX)
                vDifferent.y = 0;

            if(Vector2.SqrMagnitude(vDifferent) <= (m_fMinDistance * m_fMinDistance))
            {
                switch (hData.m_eMoveWay)
                {
                    case MoveWayType.Forward:
                        hData.m_nNextPointIndex++;
                        break;

                    case MoveWayType.Backward:
                        hData.m_nNextPointIndex--;
                        break;
                }

                goto CheckWay;
            }


            Vector3 vMove = vDifferent.normalized * hActorData.m_hStatus.status.m_fMoveSpeed * Time.deltaTime;
            hActorData.m_hActor.position += vMove;
            lstBehaviourData[nIndex] = hData;

            if (m_bFlipFacing)
            {
                if (vMove.x > 0 && !FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight)
                    || vMove.x < 0 && FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight))
                {
                    var hController = (DSC_ActorController)hActorData.m_hController;
                    hController.Flip();
                }
            }
        }

        #endregion

        #region Main


        #endregion

        #region Helper

        #endregion
    }
}