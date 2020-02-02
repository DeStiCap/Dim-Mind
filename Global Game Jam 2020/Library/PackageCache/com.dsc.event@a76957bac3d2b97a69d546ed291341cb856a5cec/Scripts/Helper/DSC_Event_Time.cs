using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Event
{
    public class DSC_Event_Time : MonoBehaviour
    {
        #region Events

        public void SetTimeScale(float fTimeScale)
        {
            if (fTimeScale < 0)
                fTimeScale = 0;

            Time.timeScale = fTimeScale;
        }

        #endregion
    }
}