using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class GameWinController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected UnityEvent m_hWinGameEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        private void OnEnable()
        {
            Global_GameplayManager.winGameEvent?.AddListener(OnWinGame);
        }

        private void OnDisable()
        {
            Global_GameplayManager.winGameEvent?.RemoveListener(OnWinGame);
        }

        #endregion

        #region Events

        public void OnWinGame()
        {
            m_hWinGameEvent?.Invoke();
        }

        #endregion

        #region Helper

        #endregion
    }
}