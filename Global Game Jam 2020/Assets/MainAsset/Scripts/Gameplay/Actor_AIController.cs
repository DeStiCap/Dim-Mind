using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    #region Data

    public struct AiData
    {
        public bool m_bPatterning;
        public float m_fPatternLastTime;
        public float m_fPatternEndCoolDown;
    }

    [RequireComponent(typeof(DSC_ActorController))]
    public class Actor_AIController : MonoBehaviour
    {
        

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        protected DSC_ActorController m_hActorController;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hActorController = GetComponent<DSC_ActorController>();
        }

        protected virtual void Update()
        {
            var hData = m_hActorController.actorData.m_hAiData;
            if (hData.m_bPatterning)
            {
                if(Time.time >= hData.m_fPatternLastTime + hData.m_fPatternEndCoolDown)
                {
                    hData.m_bPatterning = false;
                    m_hActorController.actorData.m_hAiData = hData;
                }
            }
        }

        #endregion



        #region Helper

        #endregion
    }
}