using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class Global_SoundManager : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected AudioSource m_hAudioSource;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        static Global_SoundManager instance
        {
            get
            {
                if (m_hInstance == null && m_bAppStart && !m_bAppQuit)
                    Debug.LogWarning("Don't have Global_SoundManager in scene.");

                return m_hInstance;
            }
        }

        public static AudioSource audioSource
        {
            get
            {
                if (instance == null)
                    return null;

                return m_hInstance.m_hAudioSource;
            }
        }

        #endregion

        static Global_SoundManager m_hInstance;
        static bool m_bAppStart;
        static bool m_bAppQuit;

        #endregion

        #region Base - Mono

        private void Awake()
        {
            if (m_hInstance == null)
            {
                m_hInstance = this;
            }
            else if (m_hInstance != this)
            {
                Destroy(this);
                return;
            }

            Application.quitting += OnAppQuit;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnAppStart()
        {
            m_bAppStart = true;
            m_bAppQuit = false;
        }

        void OnAppQuit()
        {
            Application.quitting -= OnAppQuit;

            m_bAppStart = false;
            m_bAppQuit = true;
        }

        #endregion

        #region Main

        public static void PlayOneShot(AudioClip hClip)
        {
            if (hClip == null || audioSource == null)
                return;

            m_hInstance.m_hAudioSource.PlayOneShot(hClip);
        }

        #endregion

        #region Helper

        #endregion
    }
}