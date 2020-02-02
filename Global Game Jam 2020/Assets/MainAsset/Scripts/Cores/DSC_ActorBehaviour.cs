using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace GGJ2020
{
    public class DSC_ActorBehaviour : BaseActorBehaviour<ActorData>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Condition")]
        [SerializeField] DSC_ActorBehaviourCondition[] m_arrCondition;

        [Header("Behaviour")]
        [SerializeField] protected BehaviourType m_eType;

#pragma warning restore 0649
        #endregion

        #endregion

        public override int behaviourTypeID { get { return (int)m_eType; } }

        public override bool isRunning { get; set; }

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            return m_arrCondition.PassAllCondition(hActorData,lstBehaviourData);
        }

        public override void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {

        }

        public override void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {

        }

        public override void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (isRunning)
                return;

            isRunning = true;
        }

        public override void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (!isRunning)
                return;

            isRunning = false;
        }

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {

        }

        public override void OnFixedUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {

        }

        public override void OnLateUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {

        }

    }
}