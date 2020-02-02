using UnityEditor;

namespace DSC.Event.Editor
{
    internal class ScriptTemplates
    {
        public const string TemplatesRoot = "Packages/com.dsc.event/Scripts/Editor/ScriptTemplates";

        [MenuItem("Assets/Create/DSC Script/Event/Event Condition")]
        public static void CreateBehaviour()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                $"{TemplatesRoot}/EventCondition.txt",
                "EventCondition.cs");
        }
    }
}