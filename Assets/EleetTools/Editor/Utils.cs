using UnityEngine;
using UnityEditor;
using System.Collections;

public class Utils
{
    public static SceneView GetSceneView()
    {
        SceneView activeSceneView = null;
        if (SceneView.lastActiveSceneView != null)
            activeSceneView = SceneView.lastActiveSceneView;
        else if (SceneView.sceneViews.Count != 0)
            activeSceneView = SceneView.sceneViews[0] as SceneView;
        return activeSceneView;
    }

    public static float GameObjectCameraDistance = 10f;
}
