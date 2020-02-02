using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
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