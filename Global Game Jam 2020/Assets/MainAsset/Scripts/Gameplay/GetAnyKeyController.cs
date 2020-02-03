using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace GGJ2020
{
    public class GetAnyKeyController : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] UnityEvent m_hPressKeyEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                m_hPressKeyEvent?.Invoke();
            }
        }

        #endregion

        #region Helper

        #endregion
    }
}