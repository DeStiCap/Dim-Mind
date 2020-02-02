using System.Collections.Generic;
using UnityEngine;

namespace DSC.UI
{
    public class DSC_UI_CanvasGroupController : UIGroupController<Canvas>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected List<Canvas> m_lstCanvas;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override int groupID { get { return m_nGroupID; } }
        public override UIType groupType { get { return UIType.Canvas; } }
        protected override List<Canvas> componentList { get { return m_lstCanvas; } }

        #endregion

        #endregion
    }
}