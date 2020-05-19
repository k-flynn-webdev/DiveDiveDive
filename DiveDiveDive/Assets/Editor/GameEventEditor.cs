using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public string EventText;

    public override void OnInspectorGUI()
    {

        GameEvent myScript = (GameEvent)target;

        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 40f;
        EditorGUILayout.PrefixLabel("Event");
        EventText = EditorGUILayout.TextField(EventText);
        if (GUILayout.Button("Update"))
        {
            myScript.NewEvent(new GameEventObj(EventText, null));
        }
        GUILayout.EndHorizontal();

        DrawDefaultInspector();

        GUILayout.Space(10f);

        for (int i = 0, max = myScript.Events.Length; i < max; i++)
        {
            EditorGUILayout.TextField(myScript.Events[i]._type);
        }
    }
}