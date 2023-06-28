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

    private void OnEnable()
    {
        attack = target as Attack;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Draws and locks the attack type in the inspector
        ToggleAttackSelection();
        DrawObjectField(ref attack.attackStats, "Attack Stats");


        DrawEditor(attack.attackStats, ref settings, ref attack.foldSettings);

        weaponObj = GetWeaponObject();
        EditorGUILayout.Space();

        ToggleWeaponSelection();
        DrawWeaponType();
        DrawEditor(weaponObj, ref editor, ref attack.foldWeapon);
    }


    /// <summary>
    /// Draws the <c>AttackType</c> enum and locks it depending on the lock <c>bool</c>
    /// </summary>
    private void ToggleAttackSelection()
    {
        attack.lockAttackType = EditorGUILayout.Toggle("Lock Attack Type", attack.lockAttackType);

        if (attack.lockAttackType)
        {
            GUI.enabled = false;
            attack.attackT = (AttackType)EditorGUILayout.EnumPopup("Attack Type", attack.attackT);
            GUI.enabled = true;
        }
        else
            attack.attackT = (AttackType)EditorGUILayout.EnumPopup("Attack Type", attack.attackT);
    }

    /// <summary>
    /// Draws the right WeaponType depending on the <c>AttackType</c> and locks it depending on the lock <c>bool</c>
    /// </summary>
    private void ToggleWeaponSelection()
    {
        attack.lockWeaponType = EditorGUILayout.Toggle("Lock Weapon Type", attack.lockWeaponType);

        if (attack.lockWeaponType)
            GUI.enabled = false;

        if (attack.attackT == AttackType.Range)
            attack.rangeWeaponType = (RangeWeaponType)EditorGUILayout.EnumPopup("Range Weapon Type", attack.rangeWeaponType);
        else
            attack.meleeWeaponType = (MeleeWeaponType)EditorGUILayout.EnumPopup("Melee Weapon Type", attack.meleeWeaponType);

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
            case AttackType.Range:
                switch (attack.rangeWeaponType)
                {
                    case RangeWeaponType.Railgun:
                        DrawObjectField(ref attack.railgun, "Railgun");
                        return;
                    case RangeWeaponType.None:
                        return;
                    default:
                        Debug.LogWarning($"Range weapon of type {attack.rangeWeaponType} not implemented");
                        return;
                }
            case AttackType.Melee:
                switch (attack.meleeWeaponType)
                {
                    case MeleeWeaponType.Sword:
                        return;
                    default:
                        Debug.LogWarning($"Melee weapon of type {attack.meleeWeaponType} not implemented");
                        return;
                }
            default:
                Debug.LogWarning($"{attack.attackT} attack type not implemented");
                return;
        }
    }

    private void DrawEditor(Object attack, ref Editor editor, ref bool fold)
    {
        if (attack == null)
            return;

        fold = EditorGUILayout.InspectorTitlebar(fold, attack);

        if (!fold)
            return;

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            CreateCachedEditor(attack, null, ref editor);
            editor.OnInspectorGUI();
        }
    }

    private Object GetWeaponObject()
    {
        switch (attack.attackT)
        {
            case AttackType.Range:
                switch (attack.rangeWeaponType)
                {
                    case RangeWeaponType.Railgun:
                        return attack.railgun;
                    case RangeWeaponType.None:
                        return null;
                    default:
                        Debug.LogWarning($"Range weapon of type {attack.rangeWeaponType} not implemented");
                        return null;
                }
            case AttackType.Melee:
                switch (attack.meleeWeaponType)
                {
                    case MeleeWeaponType.Sword:
                        return null;
                    default:
                        Debug.LogWarning($"Melee weapon of type {attack.meleeWeaponType} not implemented");
                        return null;
                }
            default:
                Debug.LogWarning($"{attack.attackT} attack type not implemented");
                return null;
        }
    }
}
