using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
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

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected AudioClip m_hJumpSound;

#pragma warning restore 0649
        #endregion

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

            if(hActorData.m_hAnimation != null)
            {
                hActorData.m_hAnimation.m_hSpineAnimationState.SetAnimation(0, hActorData.m_hAnimation.m_sJumpStartAnimation, false);
                hActorData.m_hAnimation.m_hSpineAnimationState.AddAnimation(0, hActorData.m_hAnimation.m_sJumpingAnimation, true,0.2f);
                hActorData.m_eCurrentAnimation = ActorAnimation.Airing;
            }

            if(hActorData.m_hAudio && m_hJumpSound != null)
            {
                hActorData.m_hAudio.PlayOneShot(m_hJumpSound);
            }
        }

        #endregion
    }
}