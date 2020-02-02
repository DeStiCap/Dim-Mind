using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using DSC.Core;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByInputButton : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected UnityEvent m_hDownEvent;
        [SerializeField] protected UnityEvent m_hUpOrHoldEndEvent;

#pragma warning restore 0649
        #endregion

        protected GetInputType m_ePreviousInput;

        #endregion

        #region Events

        public void PressButton(InputAction.CallbackContext hContext)
        {
            m_ePreviousInput = InputUtility.ConvertRawValueToGetType(m_ePreviousInput, hContext.ReadValue<float>());

            ProcessRunEvent();
        }

        public void RunDownEvent()
        {
            m_hDownEvent?.Invoke();
        }

        public void RunUpOrHoldEndEvent()
        {
            m_hUpOrHoldEndEvent?.Invoke();
        }

        public void SetDownEvent(UnityEvent hDownEvent)
        {
            m_hDownEvent = hDownEvent;
        }

        public void AddDownEvent(UnityAction hAction)
        {
            m_hDownEvent?.AddListener(hAction);
        }

        public void RemoveDownEvent(UnityAction hAction)
        {
            m_hDownEvent?.RemoveListener(hAction);
        }

        public void RemoveAllDownEvent()
        {
            m_hDownEvent?.RemoveAllListeners();
        }

        public void SetUpOrHoldEndEvent(UnityEvent hEvent)
        {
            m_hUpOrHoldEndEvent = hEvent;
        }

        public void AddUpOrHoldEndEvent(UnityAction hAction)
        {
            m_hUpOrHoldEndEvent?.AddListener(hAction);
        }

        public void RemoveUpOrHoldEndEvent(UnityAction hAction)
        {
            m_hUpOrHoldEndEvent?.RemoveListener(hAction);
        }

        public void RemoveAllUpOrHoldEndEvent()
        {
            m_hUpOrHoldEndEvent?.RemoveAllListeners();
        }

        #endregion

        #region Main

        protected virtual void ProcessRunEvent()
        {
            switch (m_ePreviousInput)
            {
                case GetInputType.Down:
                    RunDownEvent();
                    break;

                case GetInputType.HoldEnd:
                case GetInputType.Up:
                    RunUpOrHoldEndEvent();
                    break;
            }
        }

        #endregion
    }
}