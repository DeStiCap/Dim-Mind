using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DSC.Event.Helper
{
    [RequireComponent(typeof(Button))]
    public class DSC_Event_RunEventsByButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected UnityEvent m_hOnDown;
        [SerializeField] protected UnityEvent m_hOnUp;
        [SerializeField] protected UnityEvent m_hOnEnter;
        [SerializeField] protected UnityEvent m_hOnExit;

#pragma warning restore 0649
        #endregion

        protected Button m_hButton;

        protected bool m_bHolding;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hButton = GetComponent<Button>();
        }

        #endregion

        #region Main

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!m_hButton.enabled)
                return;

            m_hOnDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!m_hButton.enabled)
                return;

            m_hOnUp?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!m_hButton.enabled)
                return;

            m_hOnEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!m_hButton.enabled)
                return;

            m_hOnExit?.Invoke();
        }


        #endregion
    }
}