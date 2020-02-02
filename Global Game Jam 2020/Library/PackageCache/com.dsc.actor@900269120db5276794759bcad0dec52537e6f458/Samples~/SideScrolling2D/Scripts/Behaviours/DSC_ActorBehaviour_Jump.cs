using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "Behaviour_Jump", menuName = "DSC/Actor/Behaviour/Jump")]
    public class DSC_ActorBehaviour_Jump : DSC_ActorBehaviour
    {
        #region Data

        public struct JumpCacheData : IActorBehaviourData
        {
            public UnityAction<ActorData, List<IActorBehaviourData>> m_actJump;
        }

        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new JumpCacheData
            {
                m_actJump = OnJump
            });
        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnStartBehaviour(hActorData, lstBehaviourData);

            if (lstBehaviourData.TryGetData(out JumpCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Add((InputEventType.Jump, GetInputType.Down), hOutData.m_actJump);
            }
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData(out JumpCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Remove((InputEventType.Jump, GetInputType.Down), hOutData.m_actJump);
            }

            base.OnStopBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData<JumpCacheData>(out int nOutIndex))
            {
                lstBehaviourData.RemoveAt(nOutIndex);
            }

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        #endregion

        #region Main

        protected virtual void OnJump(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (hActorData.m_hRigid == null || hActorData.m_hStatus == null)
                return;

            hActorData.m_hRigid.AddForce(new Vector2(0, hActorData.m_hStatus.status.m_fJumpForce));
        }

        #endregion
    }
}