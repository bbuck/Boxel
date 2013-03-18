using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mode1GameStateManager : MonoBehaviour 
{
    private static Mode1GameStateManager _instance;
    public static Mode1GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.name = "game_state_manager";
                _instance = go.AddComponent<Mode1GameStateManager>();
                go.AddComponent<Mode1ScoreSystem>();
            }
            return _instance;
        }
    }

    private Mode1ScoreSystem _scoreSystem;
    public Mode1ScoreSystem ScoreSystem
    {
        get
        {
            if (_scoreSystem == null)
            {
                _scoreSystem = GetComponent<Mode1ScoreSystem>();
            }
            return _scoreSystem;
        }
    }

    void OnDisable()
    {
        Destroy(gameObject);
    }
}