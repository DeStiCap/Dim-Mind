using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Dialogue
{
    public abstract class BaseDialogueReplaceData : ScriptableObject
    {
        public abstract string replaceWord { get; }
    }
}