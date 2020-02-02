using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_AudioSourceSet", menuName = "DSC/Dialogue/Events/Audio Source Set")]
    public class DialogueEvent_AudioSourceSet : DialogueEvent
    {
        #region Enum

        [System.Flags]
        protected enum AudioSourceSetType
        {
            SetPlayOrStop = 1 << 0,
            SetClip       = 1 << 1,
            SetLoop       = 1 << 2
        }

        protected enum AudioSourcePlayType
        {
            Play,
            PlayDelay,
            PlayOneShot,
            Stop
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected AudioSourceSetType m_eSetType;
        [SerializeField] protected int m_nGroupID;
        [SerializeField] protected int m_nIndex;
        [SerializeField] protected AudioSourcePlayType m_ePlayType;
        [Min(0)]
        [SerializeField] protected float m_fPlayDelay;
        [SerializeField] protected AudioClip m_hClip;
        [SerializeField] protected bool m_bLoop;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!TryGetGroupController(lstData, out var hGroupController))
                return;

            if (FlagUtility.HasFlagUnsafe(m_eSetType, AudioSourceSetType.SetClip))
                SetClip(hGroupController);

            if (FlagUtility.HasFlagUnsafe(m_eSetType, AudioSourceSetType.SetLoop))
                SetLoop(hGroupController);

            if (FlagUtility.HasFlagUnsafe(m_eSetType, AudioSourceSetType.SetPlayOrStop))
                SetPlayOrStop(hGroupController);
        }

        #endregion

        #region Main

        protected virtual void SetPlayOrStop(DSC_Dialogue_AudioSourceGroupController hGroupController)
        {
            switch (m_ePlayType)
            {
                case AudioSourcePlayType.Play:
                    hGroupController.Play(m_nIndex);
                    break;

                case AudioSourcePlayType.PlayDelay:
                    hGroupController.Play(m_nIndex, m_fPlayDelay);
                    break;

                case AudioSourcePlayType.PlayOneShot:
                    hGroupController.PlayOneShot(m_nIndex, m_hClip);
                    break;

                case AudioSourcePlayType.Stop:
                    hGroupController.Stop(m_nIndex);
                    break;
            }
        }

        protected virtual void SetClip(DSC_Dialogue_AudioSourceGroupController hGroupController)
        {
            hGroupController.SetClip(m_nIndex,m_hClip);
        }

        protected virtual void SetLoop(DSC_Dialogue_AudioSourceGroupController hGroupController)
        {
            hGroupController.SetLoop(m_nIndex,m_bLoop);
        }

        #endregion

        #region Helper

        protected DSC_Dialogue_AudioSourceGroupController GetGroupController(List<IDialogueEventData> lstData)
        {
            if (!lstData.TryGetData(out DialogueEventData_GroupController<DSC_Dialogue_AudioSourceGroupController> hOutController) || hOutController.m_lstGroupController == null)
                return null;

            for(int i = 0; i < hOutController.m_lstGroupController.Count; i++)
            {
                var hGroupController = hOutController.m_lstGroupController[i];
                if (hGroupController != null && hGroupController.groupID == m_nGroupID)
                    return hGroupController;
            }

            return null;
        }

        protected bool TryGetGroupController(List<IDialogueEventData> lstData,out DSC_Dialogue_AudioSourceGroupController hOutController)
        {
            hOutController = GetGroupController(lstData);
            return hOutController != null;
        }

        #endregion
    }
}