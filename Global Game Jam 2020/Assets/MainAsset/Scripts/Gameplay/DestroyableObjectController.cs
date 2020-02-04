using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class DestroyableObjectController : MonoBehaviour, IDestroyable
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected UnityEvent m_hDestroyEvent;
        [SerializeField] protected AudioClip m_hDestroySound;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        #endregion

        #region Main

        public void Destroy()
        {
            m_hDestroyEvent?.Invoke();

            Global_SoundManager.PlayOneShot(m_hDestroySound);

            Destroy(gameObject);
        }

        #endregion

        #region Helper

        #endregion

    }
}