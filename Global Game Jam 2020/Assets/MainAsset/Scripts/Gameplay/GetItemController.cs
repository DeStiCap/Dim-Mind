using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class GetItemController : MonoBehaviour
    {
        #region Data

        [System.Serializable]
        protected struct GetItemEventData
        {
            public UnityEvent m_hEvent;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected GetItemEventData[] m_arrEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        private void OnEnable()
        {
            Global_GameplayManager.getItemEvent?.AddListener(OnGetItem);
        }

        private void OnDisable()
        {
            Global_GameplayManager.getItemEvent?.RemoveListener(OnGetItem);
        }

        #endregion

        #region Main

        void OnGetItem(Skill eSkill)
        {
            Global_GameplayManager.myTimeScale = 0;

            switch (eSkill)
            {
                case Skill.BreakRock:
                    m_arrEvent[0].m_hEvent?.Invoke();
                    break;

                case Skill.DoubleJump:
                    m_arrEvent[1].m_hEvent?.Invoke();
                    break;

                case Skill.GroundPound:
                    m_arrEvent[2].m_hEvent?.Invoke();
                    break;

                case Skill.WallJump:
                    m_arrEvent[3].m_hEvent?.Invoke();
                    break;
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}