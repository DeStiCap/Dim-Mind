using UnityEngine;

namespace DSC.Dialogue
{
    #region Data

    [System.Serializable]
    public struct Dialogue
    {
        [TextArea]
        public string m_sDialogue;
        public string m_sTalker;

        [Header("Event")]
        public BaseDialogueEvent[] m_arrEvent;
    }

    #endregion

    public abstract class BaseDialogueData : ScriptableObject
    {
        public abstract Dialogue[] allDialogue { get; }
    }
}