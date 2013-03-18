using UnityEngine;
using System;
using System.Collections;

public class Mode1Manager : MonoBehaviour
{
    public int scoreThreshold = 1000;
    public Transform portalLocation;

    void OnEnable()
    {
        Mode1GameStateManager.Instance.ScoreSystem.ScoreChanged += ScoreChanged;
    }

    void OnDisable()
    {
        Mode1GameStateManager.Instance.ScoreSystem.ScoreChanged -= ScoreChanged;
    }

    #region helper functions

    void ScoreChanged(int newScore)
    {
        if (newScore > scoreThreshold)
        {
            GetComponent<Mode1EnemySpawner>().Spawning = false;
            Quaternion rotation = Quaternion.Euler(0, 45, 0);
            PrefabManager.Instance.CreateInstance("Mode1 Portal", portalLocation.position, rotation);
        }
    }

    #endregion helper functions
}
