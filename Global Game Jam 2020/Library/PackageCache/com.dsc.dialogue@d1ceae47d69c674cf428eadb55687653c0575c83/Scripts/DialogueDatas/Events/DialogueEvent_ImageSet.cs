using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_ImageSet", menuName = "DSC/Dialogue/Events/Image Set")]
    public class DialogueEvent_ImageSet : DialogueEvent
    {
        #region Enum

        [System.Flags]
        protected enum ImageSetType
        {
            SetSprite     =   1 << 0,
            SetPosition   =   1 << 1,
            SetSize       =   1 << 2,
            SetRotation   =   1 << 3,
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [EnumMask]
        [SerializeField] protected ImageSetType m_eType;
        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected int m_nIndex;
        [SerializeField] protected Sprite m_hSprite;
        [SerializeField] protected Vector2 m_vPosition;
        [SerializeField] protected Vector2 m_vSize;
        [SerializeField] protected bool m_bUseNativeSize;
        [SerializeField] protected Vector3 m_vRotation;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            var hGroupController = GetImageGroupController(lstData);
            if (hGroupController == null)
                return;

            if (FlagUtility.HasFlagUnsafe(m_eType, ImageSetType.SetSprite))
                SetSprite(hGroupController);

            if (FlagUtility.HasFlagUnsafe(m_eType, ImageSetType.SetPosition))
                SetPosition(hGroupController);

            if (FlagUtility.HasFlagUnsafe(m_eType, ImageSetType.SetSize))
                SetSize(hGroupController);

            if (FlagUtility.HasFlagUnsafe(m_eType, ImageSetType.SetRotation))
                SetRotation(hGroupController);
        }

        #endregion

        #region Main

        protected virtual void SetSprite(DSC_Dialogue_ImageGroupController hImageGroupController)
        {
            hImageGroupController.SetSprite(m_nIndex, m_hSprite);
        }

        protected virtual void SetPosition(DSC_Dialogue_ImageGroupController hImageGroupController)
        {
            hImageGroupController.SetPosition(m_nIndex, m_vPosition);
        }

        protected virtual void SetSize(DSC_Dialogue_ImageGroupController hImageGroupController)
        {
            if (!m_bUseNativeSize)
                hImageGroupController.SetSize(m_nIndex, m_vSize);
            else
                hImageGroupController.SetSizeToNative(m_nIndex);
        }

        protected virtual void SetRotation(DSC_Dialogue_ImageGroupController hImageGroupController)
        {
            hImageGroupController.SetRotation(m_nIndex, m_vRotation);
        }

        #endregion

        #region Helper

        protected DSC_Dialogue_ImageGroupController GetImageGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_ImageGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return null;

            for(int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if (hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }

            return null;
        }

        #endregion

    }
}