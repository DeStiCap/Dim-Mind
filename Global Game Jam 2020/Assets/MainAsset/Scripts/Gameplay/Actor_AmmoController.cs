using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace GGJ2020
{
    [RequireComponent(typeof(DSC_ActorController))]
    public class Actor_AmmoController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected KnifeFlyingController[] m_arrKnifeFlying;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public int knifeFlyingAmmo
        {
            get
            {
                if (m_arrKnifeFlying == null || m_arrKnifeFlying.Length <= 0)
                    return 0;

                int nAmmo = 0;
                for(int i = 0; i < m_arrKnifeFlying.Length; i++)
                {
                    var hKnife = m_arrKnifeFlying[i];
                    if (hKnife != null && !hKnife.gameObject.activeSelf)
                        nAmmo++;
                }

                return nAmmo;
            }
        }

        public KnifeFlyingController knifeFlying
        {
            get
            {
                if (m_arrKnifeFlying == null || m_arrKnifeFlying.Length <= 0)
                    return null;

                for(int i = 0; i < m_arrKnifeFlying.Length; i++)
                {
                    var hKnife = m_arrKnifeFlying[i];
                    if (hKnife != null && !hKnife.gameObject.activeSelf)
                        return hKnife;
                }

                return null;
            }
        }

        #endregion

        protected BaseActorController<ActorData, DSC_ActorBehaviour> m_hActorController;

        #endregion

        #region Base - Mono

        private void Awake()
        {
            m_hActorController = GetComponent<DSC_ActorController>();
            m_hActorController.AddBehaviourData(new ActorMonoData<Actor_AmmoController>
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