using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using Cinemachine;
using UnityEngine.UI;

namespace GGJ2020
{

    public class TimeOutController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] Image m_hImage;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        protected bool m_bTimeOut;
        float m_fTimeCount;

        #endregion

        #region Base - Mono



        private void OnEnable()
        {
            Global_GameplayManager.timeOut?.AddListener(OnTimeOut);
        }

        private void OnDisable()
        {
            Global_GameplayManager.timeOut?.RemoveListener(OnTimeOut);
        }

        private void Update()
        {
            if (m_bTimeOut || Global_GameplayManager.myTimeScale == 0)
                return;

            m_fTimeCount += Time.deltaTime;

            var hColor = m_hImage.color;
            hColor.a = m_fTimeCount / 40;
            m_hImage.color = hColor;

            if (m_fTimeCount >= 20)
                Global_GameplayManager.timeOut?.Invoke();

        }

        #endregion

        #region Events

        public void OnTimeOut()
        {
            m_bTimeOut = true;
            var hColor = m_hImage.color;
            hColor.a = 0;
            m_hImage.color = hColor;
        }

        #endregion

        

        #region Helper

        #endregion
    }
}