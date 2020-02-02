using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_SetEnable", menuName = "DSC/Dialogue/Events/Set Enable")]
    public class DialogueEvent_SetEnable : DialogueEvent
    {
        #region Enum

        protected enum ObjectType
        {
            GameObject,
            Image,
            RawImage,
            Button,
            Canvas,
            AudioSource,
        }

        protected enum SetType
        {
            Enable,
            Disable
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected ObjectType m_eType;
        [SerializeField] protected SetType m_eSet;
        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected int m_nIndex;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            switch (m_eType)
            {
                case ObjectType.GameObject:
                    SetGameObject(lstData);
                    break;

                case ObjectType.Image:
                    SetImage(lstData);
                    break;

                case ObjectType.RawImage:
                    SetRawImage(lstData);
                    break;

                case ObjectType.Button:
                    SetButton(lstData);
                    break;

                case ObjectType.Canvas:
                    SetCanvas(lstData);
                    break;

                case ObjectType.AudioSource:
                    SetAudioSource(lstData);
                    break;
            }
        }

        #endregion

        #region Main

        protected virtual void SetGameObject(List<IDialogueEventData> lstData)
        {
            if (!TryGetGameObjectGroupController(lstData, out var hOutController))
                return;

            bool bActive = m_eSet == SetType.Enable;
            hOutController.SetEnable(m_nIndex, bActive);
        }

        protected virtual void SetImage(List<IDialogueEventData> lstData)
        {
            if (!TryGetImageGroupController(lstData, out var hOutController))
                return;

            switch (m_eSet)
            {
                case SetType.Enable:
                    hOutController.SetEnable(m_nIndex, true);
                    break;

                case SetType.Disable:
                    hOutController.SetEnable(m_nIndex, false);
                    break;
            }
        }

        protected virtual void SetRawImage(List<IDialogueEventData> lstData)
        {
            if (!TryGetRawImageGroupController(lstData, out var hOutController))
                return;

            switch (m_eSet)
            {
                case SetType.Enable:
                    hOutController.SetEnable(m_nIndex,true);
                    break;

                case SetType.Disable:
                    hOutController.SetEnable(m_nIndex, false);
                    break;
            }
        }

        protected virtual void SetButton(List<IDialogueEventData> lstData)
        {
            if (!TryGetButtonGroupController(lstData, out var hOutController))
                return;

            switch (m_eSet)
            {
                case SetType.Enable:
                    hOutController.SetEnable(m_nIndex, true);
                    break;

                case SetType.Disable:
                    hOutController.SetEnable(m_nIndex, false);
                    break;
            }
        }

        protected virtual void SetCanvas(List<IDialogueEventData> lstData)
        {
            if (!TryGetCanvasGroupController(lstData, out var hOutController))
                return;

            switch (m_eSet)
            {
                case SetType.Enable:
                    hOutController.SetEnable(m_nIndex, true);
                    break;

                case SetType.Disable:
                    hOutController.SetEnable(m_nIndex, false);
                    break;
            }
        }

        protected virtual void SetAudioSource(List<IDialogueEventData> lstData)
        {
            if (!TryGetAudioSourceGroupController(lstData, out var hOutController))
                return;

            switch (m_eSet)
            {
                case SetType.Enable:
                    hOutController.SetEnable(m_nIndex, true);
                    break;

                case SetType.Disable:
                    hOutController.SetEnable(m_nIndex, false);
                    break;
            }
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

        protected bool TryGetImageGroupController(List<IDialogueEventData> lstData,out DSC_Dialogue_ImageGroupController hOutController)
        {
            hOutController = GetImageGroupController(lstData);

            return hOutController != null;
        }

        protected DSC_Dialogue_RawImageGroupController GetRawImageGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_RawImageGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return null;

            for(int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if (hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }

            return null;
        }

        protected bool TryGetRawImageGroupController(List<IDialogueEventData> lstData, out DSC_Dialogue_RawImageGroupController hOutController)
        {
            hOutController = GetRawImageGroupController(lstData);

            return hOutController != null;
        }

        protected DSC_Dialogue_ButtonGroupController GetButtonGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_ButtonGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return null;

            for (int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if (hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }

            return null;
        }

        protected bool TryGetButtonGroupController(List<IDialogueEventData> lstData, out DSC_Dialogue_ButtonGroupController hOutController)
        {
            hOutController = GetButtonGroupController(lstData);

            return hOutController != null;
        }

        protected DSC_Dialogue_GameObjectGroupController GetGameObjectGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_GameObjectGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return null;

            for(int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if (hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }

            return null;
        }

        protected bool TryGetGameObjectGroupController(List<IDialogueEventData> lstData,out DSC_Dialogue_GameObjectGroupController hOutController)
        {
            hOutController = GetGameObjectGroupController(lstData);
            return hOutController != null;
        }

        protected DSC_Dialogue_CanvasGroupController GetCanvasGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_CanvasGroupController> hOutData) || hOutData.m_lstGroupController == null)
                return null;

            for(int i = 0; i < hOutData.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutData.m_lstGroupController[i];
                if (hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }

            return null;
        }

        protected bool TryGetCanvasGroupController(List<IDialogueEventData> lstData, out DSC_Dialogue_CanvasGroupController hOutController)
        {
            hOutController = GetCanvasGroupController(lstData);
            return hOutController != null;
        }

        protected DSC_Dialogue_AudioSourceGroupController GetAudioSourceGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_AudioSourceGroupController> hOutGroup) || hOutGroup.m_lstGroupController == null)
                return null;

            for(int i = 0;i < hOutGroup.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutGroup.m_lstGroupController[i];
                if(hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }
            
            return null;
        }

        protected bool TryGetAudioSourceGroupController(List<IDialogueEventData> lstData,out DSC_Dialogue_AudioSourceGroupController hOutController)
        {
            hOutController = GetAudioSourceGroupController(lstData);
            return hOutController != null;
        }

        #endregion
    }
}