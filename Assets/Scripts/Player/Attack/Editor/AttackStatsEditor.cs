using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackStats))]
public class AttackStatsEditor : Editor
{
    AttackStats attackStats;
    Editor editor;
    Object weaponObj;

    private void OnEnable()
    {
        attackStats = target as AttackStats;
    }

    public override void OnInspectorGUI()
    {
        ToggleTypeSelection();
        base.OnInspectorGUI();

        GetWeaponObject(attackStats.attackType);
        DrawWeaponType(weaponObj, ref editor, ref attackStats.foldWeapon);
    }

    private void ToggleTypeSelection()
    {
        attackStats.lockAttackType = EditorGUILayout.Toggle(nameof(attackStats.lockAttackType), attackStats.lockAttackType);

        if (attackStats.lockAttackType)
        {
            GUI.enabled = false;
            attackStats.attackType = (AttackType)EditorGUILayout.EnumPopup(nameof(attackStats.attackType), attackStats.attackType);
            GUI.enabled = true;
        }
        else
            attackStats.attackType = (AttackType)EditorGUILayout.EnumPopup(nameof(attackStats.attackType), attackStats.attackType);
    }

    private void GetWeaponObject(AttackType type)
    {
        switch (type)
        {
            case AttackType.Range:
                switch (attackStats.rangeWeaponType)
                {
                    case RangeWeaponType.Railgun:
                        weaponObj = attackStats.railgun;
                        return;
                }
                break;
            case AttackType.Melee:
                switch (attackStats.meleeWeaponType)
                {
                    case MeleeWeaponType.Sword:
                        return;
                }
                break;
            default:
                Debug.Log($"{type} attack type not implemnted");
                break;
        }
    }

    private void DrawWeaponType(Object weapon, ref Editor editor, ref bool fold)
    {
        if (weapon == null)
            return;

        fold = EditorGUILayout.InspectorTitlebar(fold, weapon);

        if (!fold)
            return;

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            CreateCachedEditor(weapon, null, ref editor);
            editor.OnInspectorGUI();
        }
    }

}
