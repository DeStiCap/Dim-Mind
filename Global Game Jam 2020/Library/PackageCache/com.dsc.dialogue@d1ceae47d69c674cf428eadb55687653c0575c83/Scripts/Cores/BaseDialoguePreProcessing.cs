using UnityEngine;

namespace DSC.Dialogue
{
    public abstract class BaseDialoguePreProcessing : MonoBehaviour
    {
        public abstract void PreProcessingDialogue(ref Dialogue hDialogue); 
    }
}