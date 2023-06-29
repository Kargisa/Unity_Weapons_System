using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Attack))]
public class AttackEditor : Editor
{
    Attack attack;
    Editor settings;
    Editor editor;
    Object weaponObj;
    Object statsObj;

    private void OnEnable()
    {
        attack = target as Attack;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Draws and locks the attack type in the inspector
        ToggleStatsSelection();
        DrawStatsType();
        statsObj = GetStatsObject();
        DrawEditor(statsObj, ref settings, ref attack.foldSettings);

        EditorGUILayout.Space();

        ToggleWeaponSelection();
        DrawWeaponType();
        weaponObj = GetWeaponObject();
        DrawEditor(weaponObj, ref editor, ref attack.foldWeapon);
    }


    /// <summary>
    /// Draws the <c>AttackType</c> enum and locks it depending on the lock <c>bool</c>
    /// </summary>
    private void ToggleStatsSelection()
    {
        attack.lockAttackType = EditorGUILayout.Toggle("Lock Attack Type", attack.lockAttackType);

        if (attack.lockAttackType)
            GUI.enabled = false;
        
        attack.attackT = (AttackType)EditorGUILayout.EnumPopup("Attack Type", attack.attackT);
        GUI.enabled = true;
    }

    /// <summary>
    /// Draws the right WeaponType depending on the <c>AttackType</c> and locks it depending on the lock <c>bool</c>
    /// </summary>
    private void ToggleWeaponSelection()
    {
        attack.lockWeaponType = EditorGUILayout.Toggle("Lock Weapon Type", attack.lockWeaponType);

        if (attack.lockWeaponType)
            GUI.enabled = false;
        switch (attack.attackT)
        {
            case AttackType.RangeHitscan:
                attack.rangeHitscanWeaponType = (RangeWeaponType)EditorGUILayout.EnumPopup("Range Hitscan Weapon Type", attack.rangeHitscanWeaponType);
                break;
            case AttackType.Melee:
                attack.meleeWeaponType = (MeleeWeaponType)EditorGUILayout.EnumPopup("Melee Weapon Type", attack.meleeWeaponType);
                break;
            case AttackType.RangeBullets:
                break;
            default:
                break;
        }
        GUI.enabled = true;
    }


    /// <summary>
    /// Draws a Unity Object to the inspector
    /// </summary>
    /// <typeparam name="T">Type of the Object</typeparam>
    /// <param name="obj">The Object that is going to be draw in the inspector</param>
    /// <param name="name">Label name</param>
    private void DrawObjectField<T>(ref T obj, string name) where T : Object
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(name);
        obj = EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects: true) as T;
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Only draws the selected weapon type
    /// </summary>
    private void DrawWeaponType()
    {
        switch (attack.attackT)
        {
            case AttackType.RangeHitscan:
                switch (attack.rangeHitscanWeaponType)
                {
                    case RangeWeaponType.Railgun:
                        DrawObjectField(ref attack.railgun, "Railgun");
                        return;
                    case RangeWeaponType.None:
                        return;
                    default:
                        throw new System.NotImplementedException($"Range weapon of type {attack.rangeHitscanWeaponType} not implemented");
                }
            case AttackType.Melee:
                switch (attack.meleeWeaponType)
                {
                    case MeleeWeaponType.Sword:
                        return;
                    default:
                        throw new System.NotImplementedException($"Melee weapon of type {attack.meleeWeaponType} not implemented");
                }
            case AttackType.RangeBullets:
                return;
            default:
                throw new System.NotImplementedException($"{attack.attackT} attack type not implemented");
        }
    }

    private void DrawStatsType()
    {
        switch (attack.attackT)
        {
            case AttackType.RangeHitscan:
                DrawObjectField(ref attack.rangeAttackStats, "Range Hitscan Attack Stats");
                break;
            case AttackType.Melee:
                DrawObjectField(ref attack.meleeAttackStats, "Melee Attack Stats");
                break;
            case AttackType.RangeBullets:
                break;
            default:
                throw new System.NotImplementedException($"{attack.attackT} attack type not implemented");
        }
    }

    private void DrawEditor(Object obj, ref Editor editor, ref bool fold)
    {
        if (obj == null)
            return;

        fold = EditorGUILayout.InspectorTitlebar(fold, obj);

        if (!fold)
            return;

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            CreateCachedEditor(obj, null, ref editor);
            editor.OnInspectorGUI();
        }
    }

    private Object GetWeaponObject()
    {
        switch (attack.attackT)
        {
            case AttackType.RangeHitscan:
                return attack.rangeHitscanWeaponType switch
                {
                    RangeWeaponType.Railgun => attack.railgun,
                    RangeWeaponType.None => null,
                    _ => throw new System.NotImplementedException($"Range weapon of type {attack.rangeHitscanWeaponType} not implemented"),
                };
            case AttackType.Melee:
                return attack.meleeWeaponType switch
                {
                    MeleeWeaponType.Sword => null,
                    _ => throw new System.NotImplementedException($"Melee weapon of type {attack.meleeWeaponType} not implemented"),
                };
            case AttackType.RangeBullets:
                throw new System.NotImplementedException($"{AttackType.RangeBullets} not implemented");
            default:
                throw new System.NotImplementedException($"{attack.attackT} attack type not implemented");
        }
    }

    private Object GetStatsObject()
    {
        return attack.attackT switch
        {
            AttackType.RangeHitscan => attack.rangeAttackStats,
            AttackType.Melee => attack.meleeAttackStats,
            AttackType.RangeBullets => null,
            _ => throw new System.NotImplementedException($"{attack.attackT} attack type not implemented"),
        };
    }
}
