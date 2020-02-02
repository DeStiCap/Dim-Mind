using UnityEditor;

namespace DSC.Core.Editor
{
    internal class ScriptTemplates
    {
        public const string TemplatesRoot = "Packages/com.dsc.core/Scripts/Editor/ScriptTemplates";

        [MenuItem("Assets/Create/DSC Script/Core/MonoBehaviour")]
        public static void CreateBehaviour()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                $"{TemplatesRoot}/MonoBehaviourTemplate.txt",
                "NewMonoBehaviour.cs");
        }

        [MenuItem("Assets/Create/DSC Script/Core/ScriptableObject")]
        public static void CreateScriptable()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                $"{TemplatesRoot}/ScriptableObjectTemplate.txt",
                "NewScriptableObject.cs");
        }

        [MenuItem("Assets/Create/DSC Script/Core/Global Manager")]
        public static void CreateGlobalManager()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                $"{TemplatesRoot}/GlobalManagerTemplate.txt",
                "NewGlobalManager.cs");
        }
    }
}