using UnityEngine;
using System;
using System.Collections;

public class Mode1Manager : MonoBehaviour
{
    public int scoreThreshold = 1000;
    public Transform portalLocation;

    void OnEnable()
    {
        GetComponent<Mode1ScoreSystem>().ScoreChanged += ScoreChanged;
    }

    void OnDisable()
    {
        GetComponent<Mode1ScoreSystem>().ScoreChanged -= ScoreChanged;
    }

    #region helper functions

    void ScoreChanged(int newScore)
    {
        if (newScore > scoreThreshold)
        {
            GetComponent<Mode1EnemySpawner>().Spawning = false;
            Quaternion rotation = Quaternion.Euler(0, -45, 0);
            GameObject portalObj = PrefabManager.Instance.CreateInstance("Mode1 Portal", portalLocation.position, rotation);
            portalObj.transform.parent = portalLocation;
        }
    }

    #endregion helper functions
}
