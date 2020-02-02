using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    public class Actor_SpineAnimationController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_ActorController m_hActorController;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        private void Awake()
        {
            //m_hActorController.actorData.l
        }

        #endregion

        #region Helper

        #endregion
    }
}