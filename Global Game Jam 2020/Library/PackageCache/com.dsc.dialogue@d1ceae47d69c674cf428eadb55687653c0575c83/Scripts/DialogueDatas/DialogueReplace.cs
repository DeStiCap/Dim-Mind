using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    #region Enum

    public enum ReplaceEventType
    {
        Replace,
        Color,
        ReplaceColor,
    }

    public enum IgnoreReplaceType
    {
        None,
        Dialogue,
        Talker,
    }

    public enum IgnoreColorType
    {
        None,
        Dialogue,
        Talker,
    }

    #endregion

    [CreateAssetMenu(fileName = "DialogueReplace", menuName = "DSC/Dialogue/Replace")]
    public class DialogueReplace : BaseDialogueReplace
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected string m_sReplaceID;
        [SerializeField] protected ReplaceEventType m_eEventType;
        [SerializeField] protected BaseDialogueReplaceData m_hReplaceData;

        [ColorHtml]
        [SerializeField] protected string m_sColor;

        [Header("Option")]
        [SerializeField] protected IgnoreReplaceType m_eIgnoreType;
        [SerializeField] protected IgnoreColorType m_eIgnoreColor;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override string id { get { return m_sReplaceID; } }
        public override BaseDialogueReplaceData replaceData { get { return m_hReplaceData; } }
        public override ReplaceEventType replaceType { get { return m_eEventType; } }
        public override string replaceColor { get { return m_sColor; } }
        public override IgnoreReplaceType ignoreType { get { return m_eIgnoreType; } }
        public override IgnoreColorType ignoreColor { get { return m_eIgnoreColor; } }

        #endregion

        #endregion
    }
}