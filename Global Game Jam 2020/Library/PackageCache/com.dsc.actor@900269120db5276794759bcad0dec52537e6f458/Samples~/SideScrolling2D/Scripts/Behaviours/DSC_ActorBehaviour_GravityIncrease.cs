using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "Behaviour_GravityIncrease", menuName = "DSC/Actor/Behaviour/Gravity Increase")]
    public class DSC_ActorBehaviour_GravityIncrease : DSC_ActorBehaviour
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [Min(0)]
        [SerializeField] protected float m_fGravityMultiplier = 3f;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnFixedUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnFixedUpdateBehaviour(hActorData, lstBehaviourData);

            OnGravityIncrease(hActorData, lstBehaviourData);
        }

        #endregion

        #region Main

        protected virtual void OnGravityIncrease(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            if (hActorData.m_hRigid == null)
                return;

            Vector2 vVelocity = hActorData.m_hRigid.velocity;
            vVelocity.y += Physics2D.gravity.y * hActorData.m_hRigid.gravityScale * m_fGravityMultiplier * Time.deltaTime;
            hActorData.m_hRigid.velocity = vVelocity;
        }

        #endregion
    }
}