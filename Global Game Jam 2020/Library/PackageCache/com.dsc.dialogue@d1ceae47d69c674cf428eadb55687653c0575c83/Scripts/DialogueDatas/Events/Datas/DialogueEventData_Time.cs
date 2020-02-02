using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    public struct DialogueEventData_Time : IDialogueEventData
    {
        public float m_fDialogueDataStartTime;
        public float m_fCurrentDialogueStartTime;
    }
}