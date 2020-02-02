using UnityEngine;

namespace DSC.Dialogue
{
    public struct DialogueEventData_MonoBehaviour<Mono> : IDialogueEventData where Mono : MonoBehaviour
    {
        public Mono m_hMono;
    }
}