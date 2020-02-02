using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;
using DSC.Event;

namespace DSC.Template.Actor.SideScrolling2D
{
    public class ActorData
    {
        public Transform m_hActor;
        public Rigidbody2D m_hRigid;

        public BaseActorController<ActorData, DSC_ActorBehaviour> m_hController;
        public BaseActorStatus<ActorStatus> m_hStatus;

        public ActorStateFlag m_eStateFlag;
        public InputData m_hInputData;

        public ActorData(Transform hActor)
        {
            if (hActor == null)
                return;

            m_hActor = hActor;
            m_hController = hActor.GetComponent<BaseActorController<ActorData, DSC_ActorBehaviour>>();

            InitData();
        }

        public ActorData(Transform hActor, BaseActorController<ActorData, DSC_ActorBehaviour> hController)
        {
            m_hActor = hActor;
            m_hController = hController;

            InitData();
        }

        protected virtual void InitData()
        {
            if (m_hActor == null)
                return;

            m_hRigid = m_hActor.GetComponent<Rigidbody2D>();

            m_hStatus = m_hActor.GetComponent<BaseActorStatus<ActorStatus>>();

            m_hInputData = new InputData
            {
                m_hInputCallback = new EventCallback<(InputEventType, GetInputType), ActorData, List<IActorBehaviourData>>()
            };
        }
    }
}