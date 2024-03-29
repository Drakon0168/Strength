﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityList))]
public class AbilityListEditor : Editor
{
    SerializedProperty list;

    private void OnEnable()
    {
        list = serializedObject.FindProperty("list");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUILayout.BeginVertical();
        for (int i = 0; i < list.arraySize; i++)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                list.DeleteArrayElementAtIndex(i);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        if (GUILayout.Button("Add Ability"))
        {
            list.InsertArrayElementAtIndex(list.arraySize);
            Debug.Log(list.arraySize);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
