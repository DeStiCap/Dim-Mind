using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DSC.UI
{
    public class DSC_UI_ButtonGroupController : UIGroupController<Button>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected List<Button> m_lstButton;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override int groupID { get { return m_nGroupID; } }
        public override UIType groupType { get { return UIType.Button; } }
        protected override List<Button> componentList { get { return m_lstButton; } }

        #endregion

        #endregion
    }
}