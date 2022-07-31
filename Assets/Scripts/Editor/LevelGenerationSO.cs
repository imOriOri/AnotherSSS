using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapProcess))]
public class LevelGenerationSO : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapProcess itemtrigger = (MapProcess)target;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("¸Ê »ý¼º", GUILayout.Width(120), GUILayout.Height(30)))
        {
            itemtrigger.NextLevel();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

    }
}
