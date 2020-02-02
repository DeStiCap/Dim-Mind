using UnityEngine;

namespace DSC.Dialogue
{
    public abstract class BaseDialogueReplace : ScriptableObject
    {
        public abstract string id { get; }
        public abstract ReplaceEventType replaceType { get; }
        public abstract BaseDialogueReplaceData replaceData { get; }
        public abstract string replaceColor { get; }
        public abstract IgnoreReplaceType ignoreType { get; }
        public abstract IgnoreColorType ignoreColor { get; }
    }
}