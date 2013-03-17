using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(PyramidMesh))]
[CanEditMultipleObjects]
public class PyramidMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        PyramidMesh pyramid = target as PyramidMesh;

        EditorGUILayout.BeginHorizontal();
        string[] typeNames = Enum.GetNames(typeof(PyramidType));
        PyramidType[] typeValues = Enum.GetValues(typeof(PyramidType)) as PyramidType[];
        int selected = -1;
        for (int i = 0, len = typeValues.Length; i < len; i++)
        {
            if (typeValues[i] == pyramid.Type)
            {
                selected = i;
                break;
            }
        }
        int index = EditorGUILayout.Popup("Pyramid Type", selected, typeNames);
        PyramidType newType = typeValues[index];
        if (newType != pyramid.Type)
        {
            Undo.RegisterUndo(pyramid, "Undo pyramid type change");
            pyramid.Type = newType;
        }
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        float width = EditorGUILayout.FloatField("Width", pyramid.Width);
        if (width != pyramid.Width)
        {
            Undo.RegisterUndo(pyramid, "Undo pyramid width change");
            pyramid.Width = width;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        float height = EditorGUILayout.FloatField("Height", pyramid.Height);
        if (height != pyramid.Height)
        {
            Undo.RegisterUndo(pyramid, "Undo pyramid height change");
            pyramid.Height = height;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        float depth = EditorGUILayout.FloatField("Depth", pyramid.Depth);
        if (depth != pyramid.Depth)
        {
            Undo.RegisterUndo(pyramid, "Undo pyramid depth change");
            pyramid.Depth = depth;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Mesh"))
        {
            pyramid.GenerateMesh();
        }
        EditorGUILayout.EndHorizontal();
    }
}
