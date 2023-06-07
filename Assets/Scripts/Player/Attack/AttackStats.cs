using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapon/Stats")]
public class AttackStats : ScriptableObject
{
    [HideInInspector] public bool lockAttackType;
    [HideInInspector] public AttackType attackType;

    [ConditionalHide("attackType", 0)]
    public AttackSettings.Range range;

    [ConditionalHide("attackType", 1)]
    public AttackSettings.Melee melee;

}

#region EDITOR
#if UNITY_EDITOR
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
#endregion