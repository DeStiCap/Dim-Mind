using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DSC.Actor;
using DSC.Core;

namespace GGJ2020
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
                {
                    if (m_hActorController != null)
                        m_hActorData = m_hActorController.actorData;
                    else
                    {
                        m_hActorController = GetComponent<BaseActorController<ActorData, DSC_ActorBehaviour>>();
                        if (m_hActorController)
                            m_hActorData = m_hActorController.actorData;
                    }

                    if (m_hActorData == null)
                        Debug.LogWarning("Can't update input because not have Character Data.");
                }

                return m_hActorData;
            }
        }

        protected override Dictionary<InputEventType, GetInputType> previousGetType { get { return m_dicPreviousGetType; } set { m_dicPreviousGetType = value; } }

        #endregion

        protected ActorData m_hActorData;
        protected BaseActorController<ActorData, DSC_ActorBehaviour> m_hActorController;

        protected Dictionary<InputEventType, GetInputType> m_dicPreviousGetType = new Dictionary<InputEventType, GetInputType>();

        protected PlayerInput m_hInput;

        protected InputAction m_hMove;

        protected InputAction m_hJump;
        protected InputAction m_hAttack;

        protected float m_fJumpInputLastTime;
        protected float m_fJumpInputChangeLastTime;
        protected float m_fLastJumpInputNumber;

        protected float m_fAttackInputLastTime;
        protected float m_fAttackInputChangeLastTime;
        protected float m_fLastAttackInputNumber;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hInput = GetComponent<PlayerInput>();
            InitInputAction();
        }

        protected virtual void Update()
        {
            if (m_hMove != null && m_hMove.enabled)
            {
                Vector2 vMove = m_hMove.ReadValue<Vector2>();
                UpdateInputMoveAxis(vMove.x, vMove.y);
            }

            if(m_hJump != null && m_hJump.enabled)
            {
                float fJumpNumber = m_hJump.ReadValue<float>();
                if(fJumpNumber != m_fLastJumpInputNumber)
                {
                    m_fJumpInputChangeLastTime = Time.time;

                    if(m_fJumpInputChangeLastTime - m_fJumpInputLastTime > 0.2f)
                    {
                        FixInput();
                    }
                }
                m_fLastJumpInputNumber = fJumpNumber;
            }

            if (m_hAttack != null && m_hAttack.enabled)
            {
                float fAttackNumber = m_hAttack.ReadValue<float>();
                if (fAttackNumber != m_fLastAttackInputNumber)
                {
                    m_fAttackInputChangeLastTime = Time.time;

                    if(m_fAttackInputChangeLastTime - m_fAttackInputLastTime > 0.2f)
                    {
                        FixInput();
                    }
                }
                m_fAttackInputChangeLastTime = fAttackNumber;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                FixInput();
            }
        }

        #endregion

        #region Events

        public virtual void OnJump(InputAction.CallbackContext hContext)
        {
            m_fJumpInputLastTime = Time.time;


            var eGet = OnPressInput(InputEventType.Jump, hContext.ReadValue<float>());

            

            if(eGet == GetInputType.Down && FlagUtility.HasFlagUnsafe(m_hActorData.m_eStateFlag,ActorStateFlag.IsGrounding))
            {
                m_hActorData.m_eStateFlag |= ActorStateFlag.HoldingJump;
            }
            else if(eGet == GetInputType.HoldEnd || eGet == GetInputType.Up)
            {
                m_hActorData.m_eStateFlag &= ~ActorStateFlag.HoldingJump;
            }
        }

        public virtual void OnAttack(InputAction.CallbackContext hContext)
        {
            m_fAttackInputLastTime = Time.time;

            var eGet = OnPressInput(InputEventType.Attack, hContext.ReadValue<float>());
        }

        #endregion

        #region Main

        protected virtual void UpdateInputMoveAxis(float fHorizontal,float fVertical)
        {
            if (actorData == null)
                return;

           

            var hInput = m_hActorData.m_hInputData;

            if(fHorizontal != 0 || fVertical != 0)
            {
                hInput.m_bPressInput = true;
            }

            InputUtility.CalculateAxis(ref hInput.m_fHorizontal, fHorizontal, m_fSensitivity, m_fGravity, Time.deltaTime);
            InputUtility.CalculateAxis(ref hInput.m_fVertical, fVertical, m_fSensitivity, m_fGravity, Time.deltaTime);
            m_hActorData.m_hInputData = hInput;
        }

        #endregion

        #region Helper

        protected virtual void InitInputAction()
        {
            var hAction = m_hInput.actions;
            
            var hMap = hAction.FindActionMap("Gameplay");
            m_hMove = hMap.FindAction("Move");

            m_hJump = hMap.FindAction("Jump");
            m_hAttack = hMap.FindAction("Attack");
        }

        protected override void RunEventInput(InputEventType eEventType, GetInputType eGetType)
        {
            if (actorData == null)
                return;

            var hInput = m_hActorData.m_hInputData;
            hInput.m_bPressInput = true;
            m_hActorData.m_hInputData = hInput;
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

        #region Fix

        void FixInput()
        {
            m_hMove.Disable();
            m_hMove.Enable();
            m_hJump.Disable();
            m_hJump.Enable();
            m_hAttack.Disable();
            m_hAttack.Enable();

            Debug.Log("FIX INPUT!!");
        }

        #endregion
    }
}