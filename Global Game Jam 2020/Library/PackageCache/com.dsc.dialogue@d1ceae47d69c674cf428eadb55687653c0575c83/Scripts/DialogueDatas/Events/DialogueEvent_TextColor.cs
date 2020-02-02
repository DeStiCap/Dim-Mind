using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_TextColor", menuName = "DSC/Dialogue/Events/Text Color")]
    public class DialogueEvent_TextColor : DialogueEvent
    {
        #region Enum

        protected enum TextEventType
        {
            SetDialogueColor,
            ResetDialogueColor,
            SetTalkerColor,
            ResetTalkerColor,
            SetTextColor,
            ResetTextColor,
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected TextEventType m_eType;
        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected int m_nIndex;
        [SerializeField] protected Color m_hColor;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_TextGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return;

            for(int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if(hGroupController != null && hGroupController.groupID == m_nGroupID)
                {
                    switch (m_eType)
                    {
                        case TextEventType.SetDialogueColor:
                            SetDialogueColor(hGroupController);
                            break;

                        case TextEventType.ResetDialogueColor:
                            ResetDialogueColor(hGroupController);
                            break;

                        case TextEventType.SetTalkerColor:
                            SetTalkerColor(hGroupController);
                            break;

                        case TextEventType.ResetTalkerColor:
                            ResetTalkerColor(hGroupController);
                            break;

                        case TextEventType.SetTextColor:
                            SetTextColor(hGroupController);
                            break;

                        case TextEventType.ResetTextColor:
                            ResetTextColor(hGroupController);
                            break;
                    }

                    break;
                }
            }
        }

        #endregion

        #region Main

        protected void SetDialogueColor(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            hTextGroupController.SetDialogueTextColor(m_hColor);
        }

        protected void ResetDialogueColor(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            hTextGroupController.ResetDialogueTextColorToDefault();
        }

        protected void SetTalkerColor(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            hTextGroupController.SetTalkerTextColor(m_hColor);
        }

        protected void ResetTalkerColor(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            hTextGroupController.ResetTalkerTextColorToDefault();
        }

        protected void SetTextColor(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            hTextGroupController.SetTextColor(m_nIndex, m_hColor);
        }

        protected void ResetTextColor(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            hTextGroupController.ResetTextColorToDefault(m_nIndex);
        }

        #endregion
    }
}