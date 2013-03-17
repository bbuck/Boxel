using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PrefabManager : MonoBehaviour 
{
    [HideInInspector]
    public List<string> keyList = new List<string>();

    [HideInInspector]
    public List<GameObject> prefabList = new List<GameObject>();

    public static PrefabManager Instance { get; private set; }

    private Dictionary<string, GameObject> prefabDict;

    void Start()
    {
        Instance = this;
        prefabDict = new Dictionary<string, GameObject>();
        for (int i = 0, len = keyList.Count; i < len; i++)
        {
            string key = keyList[i];
            GameObject prefab = prefabList[i];
            prefabDict[key] = prefab;
        }
    }

    #region public functions

    public GameObject CreateInstance(string prefabName, Vector3 position, Quaternion rotation)
    {
        if (!prefabDict.ContainsKey(prefabName))
            return null;
        GameObject prefab = prefabDict[prefabName];
        GameObject newObject = Instantiate(prefab, position, rotation) as GameObject;

        return newObject;
    }

    public GameObject CreateInstance(string prefabName, Vector3 position)
    {
        return CreateInstance(prefabName, position, Quaternion.identity);
    }

    public GameObject CreateInstance(string prefabName)
    {
        if (!prefabDict.ContainsKey(prefabName))
            return null;
        GameObject prefab = prefabDict[prefabName];
        GameObject newObject = Instantiate(prefab) as GameObject;

        return newObject;
    }

    #endregion public functions
}