using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(Portal))]
public class PortalEditor : Editor
{
    private bool showPortalTags = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Portal portal = target as Portal;

        EditorGUILayout.BeginHorizontal();
        showPortalTags = EditorGUILayout.Foldout(showPortalTags, "Show Portal Tags");
        EditorGUILayout.EndHorizontal();
        if (showPortalTags)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            int newSize = EditorGUILayout.IntField("Size", portal.portalTags.Count);
            if (newSize != portal.portalTags.Count)
            {
                Undo.RegisterUndo(portal, "Undo portal tag list change");
                bool remove = newSize < portal.portalTags.Count;
                int diff = Mathf.Abs(newSize - portal.portalTags.Count);
                for (int i = 0; i < diff; i++)
                {
                    if (remove)
                        portal.portalTags.RemoveAt(portal.portalTags.Count - 1);
                    else
                        portal.portalTags.Add(string.Empty);
                }
            }
            EditorGUILayout.EndHorizontal();
            for (int i = 0, len = portal.portalTags.Count; i < len; i++)
            {
                string tag = portal.portalTags[i];
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                string newTag = EditorGUILayout.TagField("Tag", tag);
                if (newTag != tag)
                {
                    Undo.RegisterUndo(portal, "Undo portal tag change");
                    portal.portalTags[i] = newTag;
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        switch (portal.type)
        {
            case PortalTypes.ToObject:
                EditorGUILayout.BeginHorizontal();
                Transform newLocation = EditorGUILayout.ObjectField("Destination", portal.destinationObject, typeof(Transform), false) as Transform;
                if (newLocation != portal.destinationObject)
                {
                    Undo.RegisterUndo(portal, "Undo destination transform change");
                    portal.destinationObject = newLocation;
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                bool isOneShot = EditorGUILayout.Toggle("One Shot", portal.isOneShot);
                if (isOneShot != portal.isOneShot)
                {
                    Undo.RegisterUndo(portal, "Undo is one shot change");
                    portal.isOneShot = isOneShot;
                }
                EditorGUILayout.EndHorizontal();
                break;
            case PortalTypes.ToLevel:
                EditorGUILayout.BeginHorizontal();
                string newSceneName = EditorGUILayout.TextField("Destination Scene", portal.destinationSceneName);
                if (newSceneName != portal.destinationSceneName)
                {
                    Undo.RegisterUndo(portal, "Undo distination scene name change");
                    portal.destinationSceneName = newSceneName;
                }
                EditorGUILayout.EndHorizontal();
                break;
        }
    }
}
