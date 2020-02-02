using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DSC.UI
{
    public class DSC_UI_RawImageGroupController : UIGroupController<RawImage>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected List<RawImage> m_lstRawImage;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override int groupID { get { return m_nGroupID; } }
        public override UIType groupType { get { return UIType.RawImage; } }
        protected override List<RawImage> componentList { get { return m_lstRawImage; } }

        #endregion

        #endregion

        #region Main

        public virtual void SetTexture(int nIndex, Texture hTexture)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hRawImage))
                return;

            hRawImage.texture = hTexture;
        }

        public virtual void SetPosition(int nIndex, Vector2 vPosition)
        {
            if (!TryGetRectTransformComponent(nIndex, out var hRectTran))
                return;

            hRectTran.anchoredPosition = vPosition;
        }

        public virtual void SetSize(int nIndex, Vector2 vSize)
        {
            if (!TryGetRectTransformComponent(nIndex, out var hRectTran))
                return;

            hRectTran.sizeDelta = vSize;
        }

        public virtual void SetSizeToNative(int nIndex)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hRawImage))
                return;

            hRawImage.SetNativeSize();
        }

        public virtual void SetRotation(int nIndex, Vector3 vRotation)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hRawImage))
                return;

            hRawImage.transform.eulerAngles = vRotation;
        }

        public virtual void SetColor(int nIndex, Color hColor)
        {
            if (!TryGetBehaviourComponent(nIndex, out var hRawImage))
                return;

            hRawImage.color = hColor;
        }

        #endregion
    }
}