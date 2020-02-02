using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;
using DSC.Event;

namespace GGJ2020
{
    #region enum

    public enum ActorAnimation
    {
        Idle,
        Running,
        Airing,
    }

    #endregion

    public class ActorData
    {
        public Transform m_hActor;
        public Rigidbody2D m_hRigid;
        public AudioSource m_hAudio;

        public BaseActorController<ActorData, DSC_ActorBehaviour> m_hController;
        public BaseActorStatus<ActorStatus> m_hStatus;
        public Actor_SpineAnimationController m_hAnimation;


        public bool m_bDefaultRightFacing;

        public Skill m_eSkill;
        public ActorStateFlag m_eStateFlag;
        public InputData m_hInputData;
        public ActorAnimation m_eCurrentAnimation;

        public AiData m_hAiData;

        public float m_fLastAttackTime;

        public EventCallback<PhysicEventType, ActorData, List<IActorBehaviourData>, Collider2D> m_hTriggerEventCallback;
        public EventCallback<PhysicEventType, ActorData, List<IActorBehaviourData>, Collision2D> m_hCollisionEventCallback;
        

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
            m_hAudio = m_hActor.GetComponent<AudioSource>();

            m_hStatus = m_hActor.GetComponent<BaseActorStatus<ActorStatus>>();

            m_hInputData = new InputData
            {
                m_hInputCallback = new EventCallback<(InputEventType, GetInputType), ActorData, List<IActorBehaviourData>>()
            };

            m_hTriggerEventCallback = new EventCallback<PhysicEventType, ActorData, List<IActorBehaviourData>, Collider2D>();
            m_hCollisionEventCallback = new EventCallback<PhysicEventType, ActorData, List<IActorBehaviourData>, Collision2D>();
        }
    }
}