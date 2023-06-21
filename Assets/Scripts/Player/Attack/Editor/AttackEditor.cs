using Codice.LogWrapper;
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

        ToggleTypeSelection();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Attack Stats");
        attack.attackStats = (AttackStats)EditorGUILayout.ObjectField(attack.attackStats, typeof(AttackStats), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();

        //UPDATE EDITOR

        DrawEditor(attack.attackStats, ref settings, ref attack.foldSettings);
        GetWeaponObject(attack.attackT);
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

    private void GetWeaponObject(AttackType type)
    {
        switch (type)
        {
            case AttackType.Range:
                switch (attack.rangeWeaponType)
                {
                    case RangeWeaponType.Railgun:
                        weaponObj = attack.railgun;
                        return;
                    case RangeWeaponType.None:
                        weaponObj = null;
                        return;
                    default:
                        weaponObj = null;
                        Debug.LogWarning($"Range weapon of type {attack.rangeWeaponType} not implemented");
                        return;
                }
            case AttackType.Melee:
                switch (attack.meleeWeaponType)
                {
                    case MeleeWeaponType.Sword:
                        return;
                    default:
                        weaponObj = null;
                        Debug.LogWarning($"Melee weapon of type {attack.meleeWeaponType} not implemented");
                        return;
                }
            default:
                weaponObj = null;
                Debug.LogWarning($"{type} attack type not implemented");
                break;
        }
    }
}
