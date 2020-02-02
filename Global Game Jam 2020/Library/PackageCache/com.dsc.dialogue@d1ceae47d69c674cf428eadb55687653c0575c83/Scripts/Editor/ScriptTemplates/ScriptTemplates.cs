using System.Collections;
using UnityEditor;

namespace DSC.Dialogue.Editor
{
    internal class ScriptTemplates
    {
        public const string TemplatesRoot = "Packages/com.dsc.dialogue/Scripts/Editor/ScriptTemplates";

        [MenuItem("Assets/Create/DSC Script/Dialogue/Dialogue Replace Data")]
        public static void CreateDialogueReplaceData()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                $"{TemplatesRoot}/DialogueReplaceDataTemplate.txt",
                "DialogueReplaceData.cs");
        }
    }
}