using UnityEngine;
using UnityEditor;

namespace DSC.Core.Editor
{
    [CustomPropertyDrawer(typeof(ColorHtmlAttribute))]
    public class ColorHtmlDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect htmlField = new Rect(position.x, position.y, position.width - 100, position.height);
            Rect colorField = new Rect(position.x + htmlField.width, position.y, position.width - htmlField.width, position.height);

            switch (property.propertyType)
            {
                case SerializedPropertyType.Color:
                    ColorType(position, property, label, htmlField, colorField);
                    break;

                case SerializedPropertyType.String:
                    StringType(position, property, label, htmlField, colorField);
                    break;
            }
        }

        void ColorType(Rect position, SerializedProperty property, GUIContent label, Rect htmlField, Rect colorField)
        {
            string htmlValue = EditorGUI.TextField(htmlField, label, "#" + ColorUtility.ToHtmlStringRGBA(property.colorValue));

            Color newColor;
            if (ColorUtility.TryParseHtmlString(htmlValue, out newColor))
                property.colorValue = newColor;

            property.colorValue = EditorGUI.ColorField(colorField, property.colorValue);
        }

        void StringType(Rect position, SerializedProperty property, GUIContent label, Rect htmlField, Rect colorField)
        {

            Color newColor;

            string htmlValue = "#" + property.stringValue;
            htmlValue = EditorGUI.TextField(htmlField, label, htmlValue);

            ColorUtility.TryParseHtmlString(htmlValue, out newColor);
            newColor = EditorGUI.ColorField(colorField, newColor);
            property.stringValue = ColorUtility.ToHtmlStringRGBA(newColor);
        }
    }
}