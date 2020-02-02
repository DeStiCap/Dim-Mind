using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueEvent_TypingSet", menuName = "DSC/Dialogue/Events/Typing Set")]
    public class DialogueEvent_TypingSet : DialogueEvent
    {
        #region Enum

        [System.Flags]
        protected enum TypingSetType
        {
            TypingDelayTime     =   1 << 0,
            TypingSound     =   1 << 1,
            EndEventDelayTime        =   1 << 2,
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected TypingSetType m_eSetType;
        [Min(0)]
        [SerializeField] protected float m_fTypingDelayTime = 0.05f;
        [SerializeField] protected AudioClip m_hTypingSound;
        [Min(0)]
        [SerializeField] protected float m_fEndEventDelayTime;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnStart(List<IDialogueEventData> lstData)
        {
            base.OnStart(lstData);

            if (!lstData.TryGetData(out DialogueEventData_MonoBehaviour<DSC_Dialogue_TypingController> hOutTyping))
                return;

            var hTyping = hOutTyping.m_hMono;

            if (FlagUtility.HasFlagUnsafe(m_eSetType, TypingSetType.TypingDelayTime))
            {
                hTyping.typingDelayTime = m_fTypingDelayTime;
            }

            if (FlagUtility.HasFlagUnsafe(m_eSetType, TypingSetType.TypingSound))
            {
                hTyping.typingSound = m_hTypingSound;
            }

            if (FlagUtility.HasFlagUnsafe(m_eSetType, TypingSetType.EndEventDelayTime))
            {
                hTyping.endEventDelayTime = m_fEndEventDelayTime;
            }
        }

        #endregion
    }
}