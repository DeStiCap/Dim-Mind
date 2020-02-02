using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_TextAlign", menuName = "DSC/Dialogue/Events/Text Align")]
    public class DialogueEvent_TextAlign : DialogueEvent
    {
        #region Enum

        protected enum TextType
        {
            Dialogue,
            Talker,
            Text,
        }

        protected enum AlignEventType
        {
            Horizontal,
            Vertical,
            Both
        }

        protected enum AlignHorizontalType
        {
            Left,
            Center,
            Right
        }

        protected enum AlignVerticalType
        {
            Top,
            Middle,
            Bottom
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649
        
        [SerializeField] protected TextType m_eTextType;
        [SerializeField] protected AlignEventType m_eAlignType;
        [SerializeField] protected AlignHorizontalType m_eHorizontal;
        [SerializeField] protected AlignVerticalType m_eVertical;
        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected int m_nIndex;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            var hTextGroupController = GetTextGroupController(lstData);
            if (hTextGroupController == null)
                return;

            switch (m_eAlignType)
            {
                case AlignEventType.Horizontal:
                    SetHorizontal(hTextGroupController);
                    break;

                case AlignEventType.Vertical:
                    SetVertical(hTextGroupController);
                    break;

                case AlignEventType.Both:
                    SetBoth(hTextGroupController);
                    break;
            }
        }

        #endregion

        #region Main

        protected virtual void SetHorizontal(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            var eVertical = AlignVerticalType.Middle;

            var eAlignType = hTextGroupController.GetTextAlign(m_nIndex);
            if (eAlignType == TextAlignmentOptions.TopLeft || eAlignType == TextAlignmentOptions.Top || eAlignType == TextAlignmentOptions.TopRight)
                eVertical = AlignVerticalType.Top;
            else if (eAlignType == TextAlignmentOptions.BottomLeft || eAlignType == TextAlignmentOptions.Bottom || eAlignType == TextAlignmentOptions.BottomRight)
                eVertical = AlignVerticalType.Bottom;

            SetTextAlign(hTextGroupController, GetAlignType(m_eHorizontal, eVertical));
        }

        protected virtual void SetVertical(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            var eHorizontal = AlignHorizontalType.Center;

            var eAlignType = hTextGroupController.GetTextAlign(m_nIndex);
            if (eAlignType == TextAlignmentOptions.TopLeft || eAlignType == TextAlignmentOptions.Left || eAlignType == TextAlignmentOptions.BottomLeft)
                eHorizontal = AlignHorizontalType.Left;
            else if (eAlignType == TextAlignmentOptions.TopRight || eAlignType == TextAlignmentOptions.Right || eAlignType == TextAlignmentOptions.BottomRight)
                eHorizontal = AlignHorizontalType.Right;

            SetTextAlign(hTextGroupController, GetAlignType(eHorizontal, m_eVertical));
        }

        protected virtual void SetBoth(DSC_Dialogue_TextGroupController hTextGroupController)
        {
            SetTextAlign(hTextGroupController, GetAlignType(m_eHorizontal, m_eVertical));
        }

        protected virtual void SetTextAlign(DSC_Dialogue_TextGroupController hTextGroupController,TextAlignmentOptions eAlign)
        {
            switch (m_eTextType)
            {
                case TextType.Dialogue:
                    hTextGroupController.SetDialogueTextAlign(eAlign);
                    break;

                case TextType.Talker:
                    hTextGroupController.SetTalkerTextAlign(eAlign);
                    break;

                case TextType.Text:
                    hTextGroupController.SetTextAlign(m_nIndex,eAlign);
                    break;
            }
        }

        #endregion

        #region Helper

        protected DSC_Dialogue_TextGroupController GetTextGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_TextGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return null;

            for(int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if (hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }

            return null;
        }

        protected TextAlignmentOptions GetAlignType(AlignHorizontalType eHorizontal,AlignVerticalType eVertical)
        {
            switch (eHorizontal)
            {
                case AlignHorizontalType.Left:
                    switch (eVertical)
                    {
                        case AlignVerticalType.Top:
                            return TextAlignmentOptions.TopLeft;

                        case AlignVerticalType.Middle:
                            return TextAlignmentOptions.Left;

                        case AlignVerticalType.Bottom:
                            return TextAlignmentOptions.BottomLeft;
                    }
                    break;

                case AlignHorizontalType.Center:
                    switch (eVertical)
                    {
                        case AlignVerticalType.Top:
                            return TextAlignmentOptions.Top;

                        case AlignVerticalType.Middle:
                            return TextAlignmentOptions.Center;

                        case AlignVerticalType.Bottom:
                            return TextAlignmentOptions.Bottom;
                    }
                    break;

                case AlignHorizontalType.Right:
                    switch (eVertical)
                    {
                        case AlignVerticalType.Top:
                            return TextAlignmentOptions.TopRight;

                        case AlignVerticalType.Middle:
                            return TextAlignmentOptions.Right;

                        case AlignVerticalType.Bottom:
                            return TextAlignmentOptions.BottomRight;
                    }
                    break;
            }

            return TextAlignmentOptions.Center;
        }

        #endregion
    }
}