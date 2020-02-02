using UnityEngine;
using UnityEditor;

namespace DSC.Core.Editor
{
    [CustomPropertyDrawer(typeof(EnumMaskAttribute))]
    public class EnumMaskDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            uint a = (uint)(EditorGUI.MaskField(position, label, property.intValue, property.enumNames));
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = (int)a;
            }
        }
    }
}