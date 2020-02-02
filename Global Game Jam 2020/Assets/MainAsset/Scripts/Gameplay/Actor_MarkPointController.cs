using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [RequireComponent(typeof(DSC_ActorController))]
    public class Actor_MarkPointController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected MarkPointController m_hMarkPointController;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public MarkPointController markPointController { get { return m_hMarkPointController; } }

        #endregion

        protected DSC_ActorController m_hActorController;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hActorController = GetComponent<DSC_ActorController>();

            m_hActorController.AddBehaviourData(new ActorMonoData<Actor_MarkPointController>
            {
                m_hMono = this
            });
        }

        #endregion

        #region Main

        #endregion

        #region Helper

        #endregion
    }
}