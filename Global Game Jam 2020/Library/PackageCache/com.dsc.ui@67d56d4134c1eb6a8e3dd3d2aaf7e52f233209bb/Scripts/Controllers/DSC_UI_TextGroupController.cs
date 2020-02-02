using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DSC.UI
{
    public class DSC_UI_TextGroupController : UIGroupController<TextMeshProUGUI>
    {
        #region Data

        protected struct TextData
        {
            public Color m_hDefaultColor;
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected List<TextMeshProUGUI> m_lstText;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override int groupID { get { return m_nGroupID; } }
        public override UIType groupType { get { return UIType.Text; } }
        protected override List<TextMeshProUGUI> componentList { get { return m_lstText; } }

        #endregion

        protected Dictionary<TextMeshProUGUI, TextData> m_dicTextData = new Dictionary<TextMeshProUGUI, TextData>();

        #endregion

        #region Main

        public virtual void SetText(int nIndex, string sText)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hText))
                return;

            hText.SetText(sText);
        }

        public virtual void SetTextColor(int nIndex, Color hColor)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hText))
                return;

            if (!m_dicTextData.ContainsKey(hText))
            {
                m_dicTextData.Add(hText, new TextData
                {
                    m_hDefaultColor = hText.color
                });
            }

            hText.color = hColor;
        }

        public virtual void ResetTextColorToDefault(int nIndex)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hText))
                return;

            if (!m_dicTextData.ContainsKey(hText))
                return;

            hText.color = m_dicTextData[hText].m_hDefaultColor;
        }

        public virtual TextAlignmentOptions GetTextAlign(int nIndex)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hText))
                return TextAlignmentOptions.Center;

            return hText.alignment;
        }

        public virtual void SetTextAlign(int nIndex, TextAlignmentOptions eAlign)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hText))
                return;

            hText.alignment = eAlign;
        }

        #endregion
    }
}