using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    #region Enum

    public enum DialogueEventType
    {
        None,
        Image,
    }

    #endregion


    public abstract class BaseDialogueEvent : ScriptableObject
    {
        public abstract void OnStart(List<IDialogueEventData> lstData);
        public abstract void OnExecute(List<IDialogueEventData> lstData);
        public abstract void OnEnd(List<IDialogueEventData> lstData);
    }
}