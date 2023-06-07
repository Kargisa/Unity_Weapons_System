using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackStats))]
public class AttackStatsEditor : Editor
{
    AttackStats attackStats;

    private void OnEnable()
    {
        attackStats = target as AttackStats;
    }

    public override void OnInspectorGUI()
    {
        ToggleTypeSelection();
        base.OnInspectorGUI();
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

}
