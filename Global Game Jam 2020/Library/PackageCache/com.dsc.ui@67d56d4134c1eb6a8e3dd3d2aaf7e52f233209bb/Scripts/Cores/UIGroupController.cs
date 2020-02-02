using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.UI
{
    public class UIGroupController<BehaviourComponent> : BaseUIGroupController where BehaviourComponent : Behaviour
    {
        #region Variable

        #region Variable - Property

        public override int groupID { get { return 0; } }
        public override UIType groupType { get { return UIType.None; } }
        protected virtual List<BehaviourComponent> componentList { get; }

        #endregion

        #endregion

        #region Base - Override

        public override void SetAllEnable(bool bEnable)
        {
            if (componentList == null)
                return;

            for (int i = 0; i < componentList.Count; i++)
            {

                var hComponent = componentList[i];
                if(hComponent != null)
                    hComponent.enabled = bEnable;
            }
        }

        public override void SetEnable(int nIndex, bool bEnable)
        {
            if (!HasIndexInListController(nIndex))
                return;

            var hComponent = componentList[nIndex];
            if (hComponent == null)
                return;

            hComponent.enabled = bEnable;
        }

        #endregion

        #region Helper

        protected bool HasIndexInListController(int nIndex)
        {
            return (nIndex >= 0 && componentList != null && componentList.Count > nIndex);
        }

        protected bool HasComponentInList(int nIndex)
        {
            return (HasIndexInListController(nIndex) && componentList[nIndex] != null);
        }

        protected BehaviourComponent GetBehaviourComponent(int nIndex)
        {
            if (nIndex < 0 || componentList == null || componentList.Count <= nIndex)
                return null;

            return componentList[nIndex];
        }

        protected bool TryGetBehaviourComponent(int nIndex, out BehaviourComponent hOutComponent)
        {
            hOutComponent = GetBehaviourComponent(nIndex);

            return hOutComponent != null;
        }

        protected RectTransform GetRectTransformComponent(int nIndex)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hComponent))
                return null;

            return (RectTransform)hComponent.transform;
        }

        protected bool TryGetRectTransformComponent(int nIndex,out RectTransform hOutRectTransform)
        {
            hOutRectTransform = GetRectTransformComponent(nIndex);
            return hOutRectTransform != null;
        }

        #endregion
    }
}