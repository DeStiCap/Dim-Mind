using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    #region Data

    [System.Serializable]
    public struct Choice
    {
        public string m_sChoice;
        public DialogueEvent[] m_arrEvent;
    }

    #endregion

    public abstract class BaseDialogueChoice : DialogueEvent
    {
        public abstract Choice[] choiceArray { get; }
    }
}