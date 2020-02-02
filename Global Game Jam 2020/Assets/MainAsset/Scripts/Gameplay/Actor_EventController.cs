using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [RequireComponent(typeof(DSC_ActorController))]
    public class Actor_EventController : MonoBehaviour
    {
        #region Data

        [System.Serializable]
        protected struct EventSetData
        {
            public UnityEvent m_hEvent;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] EventSetData[] m_arrEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        protected DSC_ActorController m_hActorController;

        #endregion

        #region Base - Mono

        private void Awake()
        {
            m_hActorController = GetComponent<DSC_ActorController>();
            m_hActorController.AddBehaviourData(new ActorMonoData<Actor_EventController>
            {
                m_hMono = this
            });
        }

        #endregion

        #region Events

        public virtual void RunEvent(int nIndex)
        {
            if (nIndex < 0 || !m_arrEvent.HasData() || m_arrEvent.Length <= nIndex)
                return;

            m_arrEvent[nIndex].m_hEvent?.Invoke();
        }

        #endregion

        #region Helper

        #endregion
    }
}