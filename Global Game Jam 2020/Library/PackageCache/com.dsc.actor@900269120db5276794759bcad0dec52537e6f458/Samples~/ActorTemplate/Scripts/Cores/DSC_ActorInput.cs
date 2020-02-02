using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DSC.Actor;
using DSC.Core;

namespace DSC.Template.Actor.Default
{
    [RequireComponent(typeof(PlayerInput))]
    public class DSC_ActorInput : BaseActorInput<ActorData, InputEventType>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Axis")]
        [Min(0)]
        [SerializeField] protected float m_fSensitivity = 3f;
        [Min(0)]
        [SerializeField] protected float m_fGravity = 3f;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        protected override ActorData actorData
        {
            get
            {
                if (m_hActorData == null)
                    Debug.LogWarning("Can't update input because not have Character Data.");

                return m_hActorData;
            }
        }

        protected override Dictionary<InputEventType, GetInputType> previousGetType { get { return m_dicPreviousGetType; } set { m_dicPreviousGetType = value; } }

        #endregion

        protected ActorData m_hActorData;
        protected BaseActorController<ActorData, DSC_ActorBehaviour> m_hActorController;

        protected Dictionary<InputEventType, GetInputType> m_dicPreviousGetType = new Dictionary<InputEventType, GetInputType>();

        protected PlayerInput m_hInput;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hInput = GetComponent<PlayerInput>();
        }

        protected virtual void Start()
        {
            m_hActorController = GetComponent<BaseActorController<ActorData, DSC_ActorBehaviour>>();
            if (m_hActorController)
            {
                m_hActorData = m_hActorController.actorData;
            }
        }

        #endregion

        #region Helper

        protected override void RunEventInput(InputEventType eEventType, GetInputType eGetType)
        {
            if (actorData == null)
                return;

            m_hActorData.m_hInputData.m_hInputCallback?.Run((eEventType, eGetType), m_hActorData, m_hActorController.listBehaviourData);
        }

        protected override void SetHoldingInputData(InputEventType eEventType, GetInputType eGetType)
        {
            if (actorData == null)
                return;

            var hInputData = m_hActorData.m_hInputData;

            InputEventType eHoldingInput = hInputData.m_eHoldingInput;

            if (eGetType == GetInputType.Down)
            {
                if (!FlagUtility.HasFlagUnsafe(eHoldingInput, eEventType))
                    eHoldingInput |= eEventType;
            }
            else if (eGetType == GetInputType.HoldEnd || eGetType == GetInputType.Up)
            {
                if (FlagUtility.HasFlagUnsafe(eHoldingInput, eEventType))
                    eHoldingInput &= ~eEventType;
            }

            hInputData.m_eHoldingInput = eHoldingInput;
            m_hActorData.m_hInputData = hInputData;
        }

        #endregion
    }
}