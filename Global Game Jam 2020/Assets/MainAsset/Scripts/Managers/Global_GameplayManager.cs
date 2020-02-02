using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DSC.Core;
using DSC.Actor;


namespace GGJ2020
{
    #region Data

    public enum Skill
    {
        BreakRock   =   1 << 0,
        DoubleJump  =   1 << 1,
        GroundPound =   1 << 2,
        WallJump    =   1 << 3,
    }

    [System.Serializable]
    public class EventSkill : UnityEvent<Skill> { }

    #endregion

    public class Global_GameplayManager : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Runtime Data")]
        [EnumMask]
        [SerializeField] Skill m_ePlayerSkill;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        static Global_GameplayManager instance
        {
            get
            {
                if (m_hInstance == null && m_bAppStart && !m_bAppQuit)
                    Debug.LogWarning("Don't have Global_GameplayManager in scene.");

                return m_hInstance;
            }
        }

        public static DSC_ActorController playerController
        {
            get
            {
                if (m_hInstance == null)
                    return null;

                return m_hInstance.m_hPlayerController;
            }

            set
            {
                if (m_hInstance == null || value == null)
                    return;

                m_hInstance.m_hPlayerController = value;
            }
        }

        public static Skill playerSkill
        {
            get
            {
                if (m_hInstance == null)
                    return 0;
                return m_hInstance.m_ePlayerSkill;

            }

            set
            {
                if (m_hInstance == null)
                    return;

                m_hInstance.m_ePlayerSkill = value;
            }
        }

        public static UnityEvent playerDeadEvent
        {
            get
            {
                if (m_hInstance == null)
                    return null;

                return m_hInstance.m_hPlayerDead;
            }
        }

        public static EventSkill getItemEvent
        {
            get
            {
                if (instance == null)
                    return null;

                return m_hInstance.m_hGetItemEvent;
            }
        }

        public static float myTimeScale
        {
            get
            {
                if (m_hInstance == null)
                    return 1;

                return m_hInstance.m_fMyTimeScale;
            }

            set
            {
                if (instance == null)
                    return;

                m_hInstance.m_fMyTimeScale = value;
                if (m_hInstance.m_fMyTimeScale < 0)
                    m_hInstance.m_fMyTimeScale = 0;
            }
        }

        public static UnityEvent timeOut
        {
            get
            {
                if (instance == null)
                    return null;

                return m_hInstance.m_hTimeOut;
            }
        }

        #endregion

        static Global_GameplayManager m_hInstance;
        static bool m_bAppStart;
        static bool m_bAppQuit;

        DSC_ActorController m_hPlayerController;

        UnityEvent m_hPlayerDead = new UnityEvent();

        EventSkill m_hGetItemEvent = new EventSkill();

        UnityEvent m_hTimeOut = new UnityEvent();

        float m_fMyTimeScale = 1;

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

        public static void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void PlayerDead()
        {
            playerDeadEvent?.Invoke();
        }

        #endregion

        #region Helper

        #endregion
    }
}