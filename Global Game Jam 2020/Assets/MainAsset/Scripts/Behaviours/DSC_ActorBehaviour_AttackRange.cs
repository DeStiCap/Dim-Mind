using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_AttackRange", menuName = "DSC/Actor/Behaviour/Attack Range")]
    public class DSC_ActorBehaviour_AttackRange : DSC_ActorBehaviour
    {
        #region Data

        public struct AttackCacheData : IActorBehaviourData
        {
            public UnityAction<ActorData, List<IActorBehaviourData>> m_actAttack;
            public float m_fAttackLastTime;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected Vector2 m_vOffset;
        [SerializeField] protected Vector2 m_vUpOffset;
        [Min(0)]
        [SerializeField] protected float m_fDelayAfterAttack;
        [SerializeField] protected AudioClip m_hThrowClip;


#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new AttackCacheData
            {
                m_actAttack = OnAttack,
                m_fAttackLastTime = -100,
            });
        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnStartBehaviour(hActorData, lstBehaviourData);

            if (lstBehaviourData.TryGetData(out AttackCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Add((InputEventType.Attack, GetInputType.Down), hOutData.m_actAttack);
            }
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData(out AttackCacheData hOutData))
            {
                hActorData.m_hInputData.m_hInputCallback.Remove((InputEventType.Attack, GetInputType.Down), hOutData.m_actAttack);
            }

            base.OnStopBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData<AttackCacheData>(out int nOutIndex))
            {
                lstBehaviourData.RemoveAt(nOutIndex);
            }

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        #endregion

        #region Main

        protected virtual void OnAttack(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (hActorData.m_hRigid == null || hActorData.m_hStatus == null || (hActorData.m_hInputData.m_fVertical < 0 && FlagUtility.HasFlagUnsafe(hActorData.m_eSkill,Skill.GroundPound)))
                return;

            if (!lstBehaviourData.TryGetData(out AttackCacheData hData, out var nIndex))
                return;

            if (!lstBehaviourData.TryGetData(out ActorMonoData<Actor_AmmoController> hAmmoController))
                return;

            if (Time.time < hData.m_fAttackLastTime + m_fDelayAfterAttack)
                return;

            var m_hKnifeFlying = hAmmoController.m_hMono.knifeFlying;

            if (m_hKnifeFlying != null)
            {
                hActorData.m_fLastAttackTime = Time.time;
                hActorData.m_eStateFlag |= ActorStateFlag.Attacking;

                m_hKnifeFlying.owner = (DSC_ActorController) hActorData.m_hController;

                // If has skill. then can break rock.
                m_hKnifeFlying.canBreakRock = FlagUtility.HasFlagUnsafe(hActorData.m_eSkill,Skill.BreakRock);

                Vector3 vPos = m_vOffset;
                bool bFacingRight = FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.FacingRight);
                Quaternion qRot;
                if (hActorData.m_hInputData.m_fVertical > 0)
                {
                    m_hKnifeFlying.flyingDirection = new Vector2(0, 1);
                    qRot = Quaternion.Euler(0, 0, 90);

                    vPos = m_vUpOffset;

                    if (!bFacingRight)
                    {
                        vPos.x = -vPos.x;
                    }
                }
                else
                {
                    if (!bFacingRight)
                    {
                        qRot = Quaternion.Euler(0, 180, 0);
                        vPos.x = -vPos.x;

                        m_hKnifeFlying.flyingDirection = new Vector2(-1, 0);
                    }
                    else
                    {
                        qRot = Quaternion.Euler(0, 0, 0);

                        m_hKnifeFlying.flyingDirection = new Vector2(1, 0);
                    }
                }

                vPos += hActorData.m_hActor.position;

                m_hKnifeFlying.transform.position = vPos;
                m_hKnifeFlying.transform.rotation = qRot;
                

                m_hKnifeFlying.gameObject.SetActive(true);

                hData.m_fAttackLastTime = Time.time;
                lstBehaviourData[nIndex] = hData;

                if (hActorData.m_hAnimation)
                {
                    hActorData.m_hAnimation.m_hSpineAnimationState.SetAnimation(1, hActorData.m_hAnimation.m_sAttackAnimation, false);
                }

                if (hActorData.m_hAudio)
                {
                    hActorData.m_hAudio.PlayOneShot(m_hThrowClip);
                }
            }
        }

        #endregion
    }
}