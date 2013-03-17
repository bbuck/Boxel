using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class PyramidGenerator 
{
    [MenuItem("GameObject/Create Other/Pyramid")]
    public static void CreatePyramid()
    {
        GameObject go = new GameObject();
        go.name = "Pyramid";
        PyramidMesh pyramid = go.AddComponent<PyramidMesh>();
        pyramid.GenerateMesh();
        BoxCollider collider = go.AddComponent<BoxCollider>();
        collider.size = Vector3.one;
        Selection.activeGameObject = go;
        SceneView sceneView = Utils.GetSceneView();
        if (sceneView != null)
            go.transform.position = sceneView.camera.transform.position + (sceneView.camera.transform.forward * Utils.GameObjectCameraDistance);
    }
}
