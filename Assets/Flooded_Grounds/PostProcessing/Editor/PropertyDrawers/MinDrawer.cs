using UnityEngine;
using UnityEngine.PostProcessing;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEditor.PostProcessing
{
    using PPMinAttribute = UnityEngine.PostProcessing.MinAttribute;

    [CustomPropertyDrawer(typeof(PPMinAttribute))]
    sealed class MinDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PPMinAttribute attribute = (PPMinAttribute)base.attribute;

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                int v = EditorGUI.IntField(position, label, property.intValue);
                property.intValue = (int)Mathf.Max(v, attribute.min);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                float v = EditorGUI.FloatField(position, label, property.floatValue);
                property.floatValue = Mathf.Max(v, attribute.min);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
            }
        }
    }
}
