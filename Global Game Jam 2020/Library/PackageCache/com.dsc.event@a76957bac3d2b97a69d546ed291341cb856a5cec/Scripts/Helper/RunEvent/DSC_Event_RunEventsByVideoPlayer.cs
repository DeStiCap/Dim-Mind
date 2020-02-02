using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace DSC.Event.Helper
{
    [RequireComponent(typeof(VideoPlayer))]
    public class DSC_Event_RunEventsByVideoPlayer : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected UnityEvent m_hOnStart;
        [SerializeField] protected UnityEvent m_hOnStop;

#pragma warning restore 0649
        #endregion

        protected VideoPlayer m_hVideo;
        protected bool m_bIsPlaying;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hVideo = GetComponent<VideoPlayer>();
        }

        protected virtual void Update()
        {
            if(m_hVideo.isPlaying != m_bIsPlaying)
            {
                if (!m_bIsPlaying)
                    OnVideoStart();
                else
                    OnVideoStop();
            }
        }

        #endregion

        #region Main

        protected void OnVideoStart()
        {
            m_bIsPlaying = true;
            m_hOnStart?.Invoke();
        }

        protected void OnVideoStop()
        {
            m_bIsPlaying = false;
            m_hOnStop?.Invoke();
        }

        #endregion
    }
}