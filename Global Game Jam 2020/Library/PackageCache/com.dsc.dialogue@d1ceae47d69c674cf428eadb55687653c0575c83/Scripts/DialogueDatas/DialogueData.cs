using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "DSC/Dialogue/Data")]
    public class DialogueData : BaseDialogueData
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] Dialogue[] m_arrDialogue;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override Dialogue[] allDialogue { get { return m_arrDialogue; } }

        #endregion

        #endregion
    }
}