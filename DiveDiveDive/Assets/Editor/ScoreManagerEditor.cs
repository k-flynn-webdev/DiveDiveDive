using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScoreManager))]
public class ScoreManagerEditor : Editor
{

    public float ScoreField = 0f;
    public string ScoreText;

    public override void OnInspectorGUI()
    {

        ScoreManager myScript = (ScoreManager)target;

        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 40f;
        EditorGUILayout.PrefixLabel("Float");
        ScoreField = EditorGUILayout.FloatField(ScoreField);
        if (GUILayout.Button("Update"))
        {
            myScript.SetScore(ScoreField);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 40f;
        EditorGUILayout.PrefixLabel("Text");
        ScoreText = EditorGUILayout.TextField(ScoreText);
        if (GUILayout.Button("Update"))
        {
            myScript.SetScore(ScoreText);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 40f;
        EditorGUILayout.PrefixLabel("Score");
        EditorGUILayout.FloatField(myScript.Score._valueFloat);
        EditorGUILayout.TextField(myScript.Score._valueText);
        GUILayout.EndHorizontal();

    }
}