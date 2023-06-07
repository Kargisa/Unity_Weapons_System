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
#if UNITY_EDITOR
    [HideInInspector] public bool foldWeapon;
#endif

    [HideInInspector] public bool lockAttackType;
    [HideInInspector] public AttackType attackType;

    [ConditionalHide(nameof(attackType), (int)AttackType.Range)]
    public AttackSettings.Range rangeSettings;

    [ConditionalHide(nameof(attackType), (int)AttackType.Melee)]
    public AttackSettings.Melee meleeSettings;


    #region Range Weapons
    //Range Weapons ScriptableObjects
    [ConditionalHide(nameof(attackType), (int)AttackType.Range)]
    public RangeWeaponType rangeWeaponType;

    [ConditionalHide(nameof(rangeWeaponType), (int)RangeWeaponType.Railgun)]
    public Railgun railgun;
    #endregion

    #region Range Weapons
    //Melee Weapons ScriptableObjects
    [ConditionalHide(nameof(attackType), (int)AttackType.Melee)]
    public MeleeWeaponType meleeWeaponType;
    #endregion
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