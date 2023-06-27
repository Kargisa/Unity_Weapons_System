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
        ToggleTypeSelection();
        DrawObjectField(ref attack.attackStats);


        //UPDATE EDITOR

        DrawEditor(attack.attackStats, ref settings, ref attack.foldSettings);
        
        EditorGUILayout.Space();
        
        weaponObj = GetWeaponObject(attack.attackT);
        if (attack.attackT == AttackType.Range)
            attack.rangeWeaponType = (RangeWeaponType)EditorGUILayout.EnumPopup("Range Weapon Type", attack.rangeWeaponType);
        else
            attack.meleeWeaponType = (MeleeWeaponType)EditorGUILayout.EnumPopup("Melee Weapon Type", attack.meleeWeaponType);
        DrawObjectField(ref weaponObj);
        DrawEditor(weaponObj, ref editor, ref attack.foldWeapon);
    }

    private void ToggleTypeSelection()
    {
        attack.lockAttackType = EditorGUILayout.Toggle(nameof(attack.lockAttackType), attack.lockAttackType);

        if (attack.lockAttackType)
        {
            GUI.enabled = false;
            attack.attackT = (AttackType)EditorGUILayout.EnumPopup("Attack Type", attack.attackT);
            GUI.enabled = true;
        }
        else
            attack.attackT = (AttackType)EditorGUILayout.EnumPopup("Attack Type", attack.attackT);
    }
    
    private void DrawObjectField<T>(ref T obj) where T : Object
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Attack Stats");
        obj = EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects: true) as T;
        EditorGUILayout.EndHorizontal();
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

    private Object GetWeaponObject(AttackType type)
    {
        switch (type)
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
                Debug.LogWarning($"{type} attack type not implemented");
                return null;
        }
    }
}
