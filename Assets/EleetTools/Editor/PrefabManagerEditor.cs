using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(PrefabManager))]
public class PrefabManagerEditor : Editor
{
    private bool showPrefabList = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PrefabManager manager = target as PrefabManager;

        EditorGUILayout.BeginHorizontal();
        showPrefabList = EditorGUILayout.Foldout(showPrefabList, "Prefab Dictionary");
        EditorGUILayout.EndHorizontal();

        if (showPrefabList)
        {
            EditorGUILayout.BeginHorizontal();
            int length = EditorGUILayout.IntField("Size:", manager.keyList.Count);
            if (length != manager.keyList.Count)
            {
                bool remove = length < manager.keyList.Count;
                int diff = Mathf.Abs(length - manager.keyList.Count);
                for (int i = 0; i < diff; i++)
                {
                    if (remove && manager.keyList.Count > 0)
                    {
                        manager.keyList.RemoveAt(manager.keyList.Count - 1);
                        manager.prefabList.RemoveAt(manager.prefabList.Count - 1);
                    }
                    else
                    {
                        manager.keyList.Add("New Key");
                        manager.prefabList.Add(null);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();
            for (int i = 0, len = manager.keyList.Count; i < len; i++)
            {
                string key = manager.keyList[i];
                GameObject prefab = manager.prefabList[i];

                EditorGUILayout.BeginHorizontal();
                key = EditorGUILayout.TextField(key);
                prefab = EditorGUILayout.ObjectField(prefab, typeof(GameObject), false) as GameObject;
                EditorGUILayout.EndHorizontal();

                manager.keyList[i] = key;
                manager.prefabList[i] = prefab;
            }
        }
    }
}
