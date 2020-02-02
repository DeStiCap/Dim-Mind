using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [CreateAssetMenu(fileName = "Behaviour_DoCollisionDamage", menuName = "DSC/Actor/Behaviour/Do Collision Damage")]
    public class DSC_ActorBehaviour_DoCollisionDamage : DSC_ActorBehaviour
    {
        #region Data

        public struct DoCollisionDamageCacheData : IActorBehaviourData
        {
            public UnityAction<ActorData, List<IActorBehaviourData>,Collider2D> m_actOnTriggerEnter;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Min(0)]
        [SerializeField] protected float m_fDamageMultiplier = 1;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnCreateBehaviour(hActorData, lstBehaviourData);

            lstBehaviourData.Add(new DoCollisionDamageCacheData
            {
                m_actOnTriggerEnter = TriggerEnter,
            });
        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnStartBehaviour(hActorData, lstBehaviourData);

            if (lstBehaviourData.TryGetData(out DoCollisionDamageCacheData hOutData))
            {
                hActorData.m_hTriggerEventCallback.Add(PhysicEventType.Enter, hOutData.m_actOnTriggerEnter);
            }
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData(out DoCollisionDamageCacheData hOutData))
            {
                hActorData.m_hTriggerEventCallback.Remove(PhysicEventType.Enter, hOutData.m_actOnTriggerEnter);
            }

            base.OnStopBehaviour(hActorData, lstBehaviourData);
        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (lstBehaviourData.TryGetData<DoCollisionDamageCacheData>(out int nOutIndex))
            {
                lstBehaviourData.RemoveAt(nOutIndex);
            }

            base.OnDestroyBehaviour(hActorData, lstBehaviourData);
        }

        #endregion

        #region Main

        protected virtual void TriggerEnter(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData, Collider2D hCollider)
        {
            if (!hCollider.gameObject.CompareTag(TagUtility.Name.player))
                return;

            var hTargetActor = hCollider.GetComponent<DSC_ActorController>();
            if (hTargetActor && FlagUtility.HasFlagUnsafe(hTargetActor.actorData.m_eStateFlag, ActorStateFlag.IsDamage))
                return;


            var hDamageable = hCollider.gameObject.GetComponent<IDamageable>();
            if(hDamageable != null)
            {
                hDamageable.DoDamage(Mathf.RoundToInt(hActorData.m_hStatus.status.m_nAttack * m_fDamageMultiplier),hActorData.m_hActor.position);
            }
        }

        #endregion
    }
}