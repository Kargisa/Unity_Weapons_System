using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHideDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHideAttribute = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHideAttribute, property);
        if (enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHideAttribute = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHideAttribute, property);

        if (enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        // Return zero height if disabled
        return 0f;
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHideAttribute, SerializedProperty property)
    {
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHideAttribute.sourceField);
        if (sourcePropertyValue != null)
        {
            return condHideAttribute.compareValue == sourcePropertyValue.enumValueIndex;
        }

        return true;
    }
}

