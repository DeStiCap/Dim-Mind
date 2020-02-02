using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DSC.UI;

namespace DSC.Dialogue
{
    public class DSC_Dialogue_TextGroupController : DSC_UI_TextGroupController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected DSC_Dialogue_DataController m_hDataController;

        [Header("Main Dialogue")]
        [SerializeField] protected TextMeshProUGUI m_hDialogueText;
        [SerializeField] protected TextMeshProUGUI m_hTalkerText;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public TextMeshProUGUI dialogueText
        {
            get
            {
                return m_hDialogueText;
            }
        }

        public TextMeshProUGUI talkerText
        {
            get
            {
                return m_hTalkerText;
            }
        }

        #endregion

        protected Color m_hDefaultDialogueTextColor;
        protected Color m_hDefaultTalkerTextColor;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            if (m_hDataController && m_hDataController.dialogueEventDataList != null)
            {
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_TextGroupController> hOutData, out int nOutIndex))
                {
                    hOutData.m_lstGroupController.Add(this);
                    m_hDataController.dialogueEventDataList[nOutIndex] = hOutData;
                }
                else
                {
                    List<DSC_Dialogue_TextGroupController> lstGroup = new List<DSC_Dialogue_TextGroupController>();
                    lstGroup.Add(this);

                    m_hDataController.dialogueEventDataList.Add(new DialogueEventData_GroupController<DSC_Dialogue_TextGroupController>
                    {
                        m_lstGroupController = lstGroup
                    });
                }
            }

            SetDialogueTextColorDefault();
            SetTalkerTextColorDefault();
        }

        /*
        protected virtual void OnDestroy()
        {
            if (m_hDataController && m_hDataController.dialogueEventDataList != null)
            {
                if (m_hDataController.dialogueEventDataList.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_TextGroupController> hOutData, out int nOutIndex))
                {
                    hOutData.m_lstGroupController.Remove(this);

                    if (hOutData.m_lstGroupController.Count > 0)
                        m_hDataController.dialogueEventDataList[nOutIndex] = hOutData;
                    else
                        m_hDataController.dialogueEventDataList.RemoveAt(nOutIndex);
                }
            }
        }*/

        #endregion

        #region Main

        public virtual void SetDialogueTextColor(Color hColor)
        {
            if (!HasDialogueText())
                return;

            m_hDialogueText.color = hColor;
        }

        public virtual void ResetDialogueTextColorToDefault()
        {
            if (!HasDialogueText())
                return;

            m_hDialogueText.color = m_hDefaultDialogueTextColor;
        }

        public virtual TextAlignmentOptions GetDialogueTextAlign()
        {
            if (!HasDialogueText())
                return TextAlignmentOptions.Center;

            return m_hDialogueText.alignment;
        }

        public virtual void SetDialogueTextAlign(TextAlignmentOptions eAlign)
        {
            if (!HasDialogueText())
                return;

            m_hDialogueText.alignment = eAlign;
        }

        public virtual void SetTalkerTextColor(Color hColor)
        {
            if (!HasTalkerText())
                return;

            m_hTalkerText.color = hColor;
        }

        public virtual void ResetTalkerTextColorToDefault()
        {
            if (!HasTalkerText())
                return;

            m_hTalkerText.color = m_hDefaultTalkerTextColor;
        }

        public virtual TextAlignmentOptions GetTalkerTextAlign()
        {
            if (!HasTalkerText())
                return TextAlignmentOptions.Center;

            return m_hTalkerText.alignment;
        }

        public virtual void SetTalkerTextAlign(TextAlignmentOptions eAlign)
        {
            if (!HasTalkerText())
                return;

            m_hTalkerText.alignment = eAlign;
        }

        #endregion

        #region Helper

        protected bool HasDialogueText()
        {
            return m_hDialogueText != null;
        }

        protected bool HasTalkerText()
        {
            return m_hTalkerText != null;
        }

        protected void SetDialogueTextColorDefault()
        {
            if (!HasDialogueText())
                return;

            m_hDialogueText.color = m_hDialogueText.color;
        }

        protected void SetTalkerTextColorDefault()
        {
            if (!HasTalkerText())
                return;

            m_hTalkerText.color = m_hTalkerText.color;
        }

        #endregion
    }
}