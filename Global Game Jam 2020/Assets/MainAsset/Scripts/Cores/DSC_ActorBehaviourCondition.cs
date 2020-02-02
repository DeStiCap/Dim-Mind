using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace GGJ2020
{
    public class DSC_ActorBehaviourCondition : BaseActorBehaviourCondition<ActorData>
    {
        [SerializeField] protected DSC_ActorBehaviourCondition[] m_arrCondition;

        public override bool PassCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            return PassAllCondition(hActorData,lstBehaviourData);
        }

        protected bool PassAllCondition(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            return m_arrCondition.PassAllCondition(hActorData, lstBehaviourData);
        }
    }
}