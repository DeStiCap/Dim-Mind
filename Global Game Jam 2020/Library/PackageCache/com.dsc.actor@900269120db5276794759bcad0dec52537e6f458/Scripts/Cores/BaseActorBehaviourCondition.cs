using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorBehaviourCondition<ActorData> : ScriptableObject
    {
        /// <summary>
        /// Check condition, return true when pass and return false when fail.
        /// </summary>
        public abstract bool PassCondition(ActorData hCharacterData, List<IActorBehaviourData> lstBehaviourData);
    }
}