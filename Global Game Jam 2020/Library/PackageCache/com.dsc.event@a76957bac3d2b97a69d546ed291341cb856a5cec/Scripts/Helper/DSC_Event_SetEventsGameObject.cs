using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Event.Helper
{
    public class DSC_Event_SetEventsGameObject : MonoBehaviour
    {
        #region Data

        [System.Serializable]
        protected class SetEvent : UnityEvent<EventGameObject> { }

        [System.Serializable]
        protected struct GroupEventSet
        {
            public SetEvent m_hSetEvent;
            public EventGameObject m_hEvents;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] GroupEventSet[] m_arrEventSet;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Events

        public void RunSetEvent(int nIndex)
        {
            if (m_arrEventSet == null)
                return;
            else if (nIndex < 0 || m_arrEventSet.Length <= nIndex)
            {
                Debug.LogError("Don't have set event index " + nIndex);
                return;
            }

            var hEventSetData = m_arrEventSet[nIndex];
            hEventSetData.m_hSetEvent?.Invoke(hEventSetData.m_hEvents);
        }

        #endregion
    }
}