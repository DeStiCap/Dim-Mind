using UnityEngine;
using TMPro;
using DSC.Core;
using DSC.UI;

namespace DSC.Dialogue
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DSC_Dialogue_TextController : BaseComponentController
    {
        #region Variable

        protected TextMeshProUGUI m_hText;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hText = GetComponent<TextMeshProUGUI>();
        }

        #endregion

        #region Events

        public virtual void SetTalkerText(Dialogue hDialogue)
        {
            m_hText.SetText(hDialogue.m_sTalker);
        }

        public virtual void SetDialogueText(Dialogue hDialogue)
        {
            m_hText.SetText(hDialogue.m_sDialogue);
        }

        public override void SetEnable(bool bEnable)
        {
            m_hText.enabled = bEnable;
        }

        #endregion
    }
}