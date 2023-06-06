using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Attack))]
public class AttackEditor : Editor
{
    Attack attack;
    Editor settings;

    private void OnEnable()
    {
        attack = target as Attack;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettings(attack.settings, ref settings, ref attack.foldSettings);
    }

    private void DrawSettings(Object attack, ref Editor editor, ref bool fold)
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
}
