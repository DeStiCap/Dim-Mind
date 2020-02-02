using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DSC.Event.Helper
{
    public class DSC_Event_RunEventsByEventGameObject : MonoBehaviour
    {
        #region Temp

        [System.Serializable]
        protected class GameObjectEvent : UnityEvent<GameObject> { }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected EventCondition[] m_arrCondition;
        [SerializeField] protected GameObjectEvent m_hPreRunEvent;
        [SerializeField] protected GameObjectEvent m_hRunEvent;
        [SerializeField] protected GameObjectEvent m_hPostRunEvent;

#pragma warning restore 0649
        #endregion

        protected EventConditionData m_hConditionData;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hConditionData = new EventConditionData(transform);
        }

        #endregion

        #region Events

        public virtual void RunEvent(GameObject hGameObject)
        {
            if (!IsPassCondition())
                return;

            m_hPreRunEvent?.Invoke(hGameObject);
            m_hRunEvent?.Invoke(hGameObject);
            m_hPostRunEvent?.Invoke(hGameObject);
        }

        public void SetCondition(params EventCondition[] arrCondition)
        {
            m_arrCondition = arrCondition;
        }

        public void AddPreRunEvent(UnityAction<GameObject> hAction)
        {
            m_hPreRunEvent?.AddListener(hAction);
        }

        public void RemovePreRunEvent(UnityAction<GameObject> hAction)
        {
            m_hPreRunEvent?.RemoveListener(hAction);
        }

        public void RemoveAllPreRunEvent()
        {
            m_hPreRunEvent?.RemoveAllListeners();
        }

        public void AddRunEvent(UnityAction<GameObject> hAction)
        {
            m_hRunEvent?.AddListener(hAction);
        }

        public void RemoveRunEvent(UnityAction<GameObject> hAction)
        {
            m_hRunEvent?.RemoveListener(hAction);
        }

        public void RemoveAllRunEvent()
        {
            m_hRunEvent?.RemoveAllListeners();
        }

        public void AddPostRunEvent(UnityAction<GameObject> hAction)
        {
            m_hPostRunEvent?.AddListener(hAction);
        }

        public void RemovePostRunEvent(UnityAction<GameObject> hAction)
        {
            m_hPostRunEvent?.RemoveListener(hAction);
        }

        public void RemoveAllPostRunEvent()
        {
            m_hPostRunEvent?.RemoveAllListeners();
        }

        #endregion

        #region Helper

        protected bool IsPassCondition()
        {
            return m_arrCondition.PassAllCondition(m_hConditionData);
        }

        #endregion
    }
}