using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class LevelGenerationSO : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapGenerator itemtrigger = (MapGenerator)target;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("¸Ê »ý¼º", GUILayout.Width(120), GUILayout.Height(30)))
        {
            itemtrigger.MapGenerate();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

    }
}
