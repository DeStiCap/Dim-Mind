using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class GameOverController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected UnityEvent m_hGameOverEvent;
        [SerializeField] protected Transform m_hDeadParticle;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        private void OnEnable()
        {
            Global_GameplayManager.playerDeadEvent?.AddListener(GameOver);
        }

        private void OnDisable()
        {
            Global_GameplayManager.playerDeadEvent?.RemoveListener(GameOver);
        }

        #endregion

        #region Events

        public void GameOver()
        {
            m_hGameOverEvent?.Invoke();

            var hPartical = Instantiate(m_hDeadParticle, Global_GameplayManager.playerController.actorData.m_hActor.position, Quaternion.identity);
        }

        #endregion

        #region Helper

        #endregion
    }
}