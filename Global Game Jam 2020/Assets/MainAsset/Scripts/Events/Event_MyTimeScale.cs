using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class Event_MyTimeScale : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        #endregion

        #endregion

        #region Base - Mono

        #endregion

        #region Events

        public void SetMyTimeScale(float fScale)
        {
            Global_GameplayManager.myTimeScale = fScale;
        }

        public void TimeOut()
        {
            Global_GameplayManager.timeOut?.Invoke();
        }

        #endregion

        #region Helper

        #endregion
    }
}