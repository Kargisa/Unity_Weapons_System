using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/AttackMaker")]
public class AttackMaker : ScriptableObject
{
    public AttackType attackType;

    [ConditionalHide("attackType", 0)]
    public AttackSettings.Range range;

    [ConditionalHide("attackType", 1)]
    public AttackSettings.Melee melee;

}

#if UNITY_EDITOR
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
            return sourcePropertyValue.enumDisplayNames[condHideAttribute.compareValue] == ((AttackType)sourcePropertyValue.enumValueIndex).ToString();
        }

        return true;
    }
}

public class ConditionalHideAttribute : PropertyAttribute
{
    public string sourceField;
    public int compareValue;

    public ConditionalHideAttribute(string sourceField, int compareValue)
    {
        this.sourceField = sourceField;
        this.compareValue = compareValue;
    }
}
#endif
