using UnityEngine;
using UnityEditor;
using System.Collections;

public class GroupSelection 
{
    [MenuItem("Utility/Group Selection %G")]
    static void GroupSelectedObjects()
    {
        GameObject[] selected = Selection.gameObjects;
        GameObject group = new GameObject("new_group");
        foreach (GameObject go in selected)
        {
            go.transform.parent = group.transform;
        }
    }
}
