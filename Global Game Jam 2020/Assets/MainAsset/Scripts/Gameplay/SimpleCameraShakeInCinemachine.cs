using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

namespace GGJ2020
{
    public class SimpleCameraShakeInCinemachine : MonoBehaviour
    {

        public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
        public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
        public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

        // Cinemachine Shake
        public CinemachineVirtualCamera VirtualCamera;
        private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

        bool m_bTimeOut;
        float m_fTimeCount;
        bool m_bPlayerDead;
        bool m_bWinGame;
        bool m_bStartGame;

        // Use this for initialization
        void Start()
        {
            // Get Virtual Camera Noise Profile
            if (VirtualCamera != null)
                virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        private void OnEnable()
        {
            Global_GameplayManager.timeOut?.AddListener(OnTimeOut);
            Global_GameplayManager.playerDeadEvent?.AddListener(OnPlayerDead);
            Global_GameplayManager.winGameEvent?.AddListener(OnWinGame);
        }

        private void OnDisable()
        {
            Global_GameplayManager.timeOut?.RemoveListener(OnTimeOut);
            Global_GameplayManager.playerDeadEvent?.RemoveListener(OnPlayerDead);
            Global_GameplayManager.winGameEvent?.RemoveListener(OnWinGame);
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_bStartGame)
                return;

            if (Global_GameplayManager.myTimeScale == 0)
            {
                ShakeAmplitude = 0;
                ShakeFrequency = 0;
            }
            else if (m_bTimeOut || m_bWinGame)
                return;
            else if (m_bPlayerDead)
            {
                ShakeAmplitude = 0;
                ShakeFrequency = 0;
            }
            else
            {
                m_fTimeCount += Time.deltaTime;

                

                if (m_fTimeCount < 10)
                    return;

                if (m_fTimeCount <= 15)
                {
                    ShakeAmplitude = m_fTimeCount / 10;
                    ShakeFrequency = m_fTimeCount / 10;
                }
                else if(m_fTimeCount < 20)
                {
                    ShakeAmplitude = m_fTimeCount / 5;
                    ShakeFrequency = m_fTimeCount / 5;
                }
                else
                {
                    ShakeAmplitude = m_fTimeCount / 2;
                    ShakeFrequency = m_fTimeCount / 2;
                }
            }
            

            

            // If the Cinemachine componet is not set, avoid update
            if (VirtualCamera != null && virtualCameraNoise != null)
            {

                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
     
            }

            if (m_fTimeCount >= 20)
            {
                Global_GameplayManager.timeOut?.Invoke();
            }
        }

        public void StartGame()
        {
            m_bStartGame = true;
        }

        public void OnPlayerDead()
        {
            m_bPlayerDead = true;
        }

        public void OnTimeOut()
        {
            m_bTimeOut = true;
        }

        public void OnWinGame()
        {
            m_bWinGame = true;


            // If the Cinemachine componet is not set, avoid update
            if (VirtualCamera != null && virtualCameraNoise != null)
            {

                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = 0;
                virtualCameraNoise.m_FrequencyGain = 0;
            }
        }

    }
}