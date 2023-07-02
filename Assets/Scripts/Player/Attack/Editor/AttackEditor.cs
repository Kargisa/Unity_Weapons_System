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
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Attack Settings", EditorStyles.boldLabel);

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
                attack.rangeHitscanWeaponType = (RangeHitscanWeaponType)EditorGUILayout.EnumPopup("Range Hitscan Weapon Type", attack.rangeHitscanWeaponType);
                break;
            case AttackType.Melee:
                attack.meleeWeaponType = (MeleeWeaponType)EditorGUILayout.EnumPopup("Melee Weapon Type", attack.meleeWeaponType);
                break;
            case AttackType.Bullet:
                attack.bulletWeaponType = (BulletWeaponType)EditorGUILayout.EnumPopup("Bullet Weapon Type", attack.bulletWeaponType);
                break;
            default:
                throw new System.NotImplementedException($"{attack.attackT} is not implemented");
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
                    case RangeHitscanWeaponType.Railgun:
                        DrawObjectField(ref attack.railgun, "Railgun");
                        return;
                    case RangeHitscanWeaponType.None:
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
            case AttackType.Bullet:
                switch (attack.bulletWeaponType)
                {
                    case BulletWeaponType.Pistol:
                        DrawObjectField(ref attack.pistol, "Pistol");
                        return;
                    default:
                        break;
                }
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
                DrawObjectField(ref attack.rangeHitscanAttackStats, "Range Hitscan Attack Stats");
                break;
            case AttackType.Melee:
                DrawObjectField(ref attack.meleeAttackStats, "Melee Attack Stats");
                break;
            case AttackType.Bullet:
                DrawObjectField(ref attack.bulletAttackStats, "Bullet Weapon Stats");
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
                    RangeHitscanWeaponType.Railgun => attack.railgun,
                    RangeHitscanWeaponType.None => null,
                    _ => throw new System.NotImplementedException($"Range Hitscan weapon of type {attack.rangeHitscanWeaponType} not implemented"),
                };
            case AttackType.Melee:
                return attack.meleeWeaponType switch
                {
                    MeleeWeaponType.Sword => null,
                    _ => throw new System.NotImplementedException($"Melee weapon of type {attack.meleeWeaponType} not implemented"),
                };
            case AttackType.Bullet:
                return attack.bulletWeaponType switch
                {
                    BulletWeaponType.Pistol => attack.pistol,
                    BulletWeaponType.None => null,
                    _ => throw new System.NotImplementedException($"Bullet weapon of type {attack.bulletWeaponType} not implemented"),
                };
            default:
                throw new System.NotImplementedException($"{attack.attackT} attack type not implemented");
        }
    }

    private Object GetStatsObject()
    {
        return attack.attackT switch
        {
            AttackType.RangeHitscan => attack.rangeHitscanAttackStats,
            AttackType.Melee => attack.meleeAttackStats,
            AttackType.Bullet => attack.bulletAttackStats,
            _ => throw new System.NotImplementedException($"{attack.attackT} attack type not implemented"),
        };
    }
}
