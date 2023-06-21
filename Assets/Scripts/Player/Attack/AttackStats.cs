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

    [HideInInspector] public AttackType attackType;

    [ConditionalHide(nameof(attackType), (int)AttackType.Range)]
    public AttackSettings.Range rangeSettings;

    [ConditionalHide(nameof(attackType), (int)AttackType.Melee)]
    public AttackSettings.Melee meleeSettings;
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