using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace Template
{
    public class Event_Debug : MonoBehaviour
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

        public void Log(string sLog)
        {
            Debug.Log(sLog);
        }

        #endregion

        #region Helper

        #endregion
    }
}