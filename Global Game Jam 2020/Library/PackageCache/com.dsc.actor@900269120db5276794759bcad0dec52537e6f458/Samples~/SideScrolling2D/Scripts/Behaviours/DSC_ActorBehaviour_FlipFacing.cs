using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "Behaviour_FlipFacing", menuName = "DSC/Actor/Behaviour/Flip Facing")]
    public class DSC_ActorBehaviour_FlipFacing : DSC_ActorBehaviour
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected float m_fRightAngle = 0;
        [SerializeField] protected float m_fLeftAngle = 180;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnUpdateBehaviour(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData)
        {
            base.OnUpdateBehaviour(hActorData, lstBehaviourData);

            float fHorizontal = hActorData.m_hInputData.m_fHorizontal;
            Vector3 vAngle = hActorData.m_hActor.localEulerAngles;
            if (fHorizontal > 0 && vAngle.y != m_fRightAngle)
            {
                vAngle.y = m_fRightAngle;
            }
            else if (fHorizontal < 0 && vAngle.y == m_fRightAngle)
            {
                vAngle.y = m_fLeftAngle;
            }

            hActorData.m_hActor.localEulerAngles = vAngle;
        }

        #endregion
    }
}