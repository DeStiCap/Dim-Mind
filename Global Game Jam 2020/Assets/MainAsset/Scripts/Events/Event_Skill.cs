using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace GGJ2020
{
    public class Event_Skill : MonoBehaviour
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

        public void ResetSkill()
        {
            Global_GameplayManager.playerSkill = 0;
        }

        #endregion

        #region Helper

        #endregion
    }
}