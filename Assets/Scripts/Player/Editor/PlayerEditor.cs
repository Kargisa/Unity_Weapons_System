using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    Player player;

    private void OnEnable()
    {
        player = (Player)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Info(ref player.foldoutInfo);
    }

    public void DrawAbilitiesEditor(Object abilitie, ref Editor editor, ref bool foldout)
    {
        if (abilitie == null)
            return;

        foldout = EditorGUILayout.InspectorTitlebar(foldout, abilitie);

        if (foldout)
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                CreateCachedEditor(abilitie, null, ref editor);
                editor.OnInspectorGUI();
            }
        }
    }

    private void Info(ref bool foldout)
    {
        foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, "Info");

        if (foldout)
        {
            GUI.enabled = false;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Is Grounded");
            EditorGUILayout.Toggle(player.grounded);
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
