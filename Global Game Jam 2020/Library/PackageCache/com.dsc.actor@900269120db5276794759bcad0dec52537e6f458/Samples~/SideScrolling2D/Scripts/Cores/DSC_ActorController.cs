using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    public class DSC_ActorController : BaseActorController<ActorData,DSC_ActorBehaviour>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected List<DSC_ActorBehaviour> m_lstBehaviour;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override ActorData actorData { get { return m_hData; } }

        protected override List<DSC_ActorBehaviour> listBehaviour { get { return m_lstBehaviour; } set { m_lstBehaviour = value; } }

        public override List<IActorBehaviourData> listBehaviourData { get { return m_lstBehaviourData; } protected set { m_lstBehaviourData = value; } }

        #endregion

        protected ActorData m_hData;

        protected List<IActorBehaviourData> m_lstBehaviourData = new List<IActorBehaviourData>();

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            // Can change this to register in manager instead.
            m_hData = new ActorData(transform,this);

            CreateAllBehaviour();
        }

        protected virtual void OnEnable()
        {
            StartAllBehaviour();
        }

        protected virtual void OnDisable()
        {
            StopAllBehaviour();
        }

        protected virtual void OnDestroy()
        {
            DestroyAllBehaviour();
        }

        protected virtual void Update()
        {
            ExecuteBehaviour();
        }

        protected virtual void FixedUpdate()
        {
            FixedExecuteBehaviour();
        }

        protected virtual void LateUpdate()
        {
            LateExecuteBehaviour();
        }

        #endregion

        #region Events

        public virtual void SetIsGrounding(bool bGrounding)
        {
            if (bGrounding)
                m_hData.m_eStateFlag |= ActorStateFlag.IsGrounding;
            else
                m_hData.m_eStateFlag &= ~ActorStateFlag.IsGrounding;
        }

        public virtual void SetIsWalling(bool bWalling)
        {
            if (bWalling)
                m_hData.m_eStateFlag |= ActorStateFlag.IsWalling;
            else
                m_hData.m_eStateFlag &= ~ActorStateFlag.IsWalling;
        }

        #endregion
    }
}