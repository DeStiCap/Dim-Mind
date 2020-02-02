using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DSC.Core;

namespace GGJ2020
{
    [RequireComponent(typeof(Image))]
    public class HurtImageController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        protected DSC_ActorController m_hActorController;

        protected Image m_hImage;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hImage = GetComponent<Image>();
        }

        protected virtual void Start()
        {
            m_hActorController = Global_GameplayManager.playerController;
            if (m_hActorController)
            {
                m_hActorController.takeDamageEvent?.AddListener(OnTakeDamage);
            }
        }

        #endregion

        #region Events

        public void OnTakeDamage()
        {
            var hColor = m_hImage.color;
            int nHpLeft = m_hActorController.actorData.m_hStatus.status.m_nHP;

            switch (nHpLeft)
            {
                case 0:
                    hColor.a = 0;
                    break;

                case 1:
                    hColor.a = 1;
                    break;

                case 2:
                    hColor.a = 0.5f;
                    break;
            }

            m_hImage.color = hColor;
        }

        #endregion

        #region Helper

        #endregion
    }
}