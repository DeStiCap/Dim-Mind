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

        private float ShakeElapsedTime = 0f;

        // Cinemachine Shake
        public CinemachineVirtualCamera VirtualCamera;
        private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

        bool m_bTimeOut;
        float m_fTimeCount;

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
        }

        private void OnDisable()
        {
            Global_GameplayManager.timeOut?.RemoveListener(OnTimeOut);
        }

        // Update is called once per frame
        void Update()
        {
            if (m_bTimeOut || Global_GameplayManager.myTimeScale == 0)
                return;

            m_fTimeCount += Time.deltaTime;

            if (m_fTimeCount >= 20)
                Global_GameplayManager.timeOut?.Invoke();

            if (m_fTimeCount < 10)
                return;

            ShakeAmplitude = m_fTimeCount / 5;
            ShakeFrequency = m_fTimeCount / 5;

            // If the Cinemachine componet is not set, avoid update
            if (VirtualCamera != null && virtualCameraNoise != null)
            {

                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
     
            }
        }

        public void OnTimeOut()
        {
            m_bTimeOut = true;
        }

    }
}