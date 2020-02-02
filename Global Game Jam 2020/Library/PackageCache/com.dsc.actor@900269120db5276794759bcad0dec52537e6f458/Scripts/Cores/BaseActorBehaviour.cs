using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorBehaviour<ActorData> : ScriptableObject
    {
        /// <summary>
        /// ID of behaviour type. For use to change,remove this behaviour type from data.
        /// </summary>
        public abstract int behaviourTypeID { get; }

        /// <summary>
        /// Set true/false for know this behaviour running currectly or not.
        /// </summary>
        public abstract bool isRunning { get; set; }

        /// <summary>
        /// Check all condition, return true when pass and return false when fail.
        /// </summary>
        public abstract bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnCreateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnDestroyBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnStartBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnFixedUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnLateUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnStopBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
    }
}