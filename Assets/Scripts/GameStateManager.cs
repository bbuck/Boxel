using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour 
{
    public bool Paused { get; set; }

    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.name = "game_state_manager";
                _instance = go.AddComponent<GameStateManager>();
            }
            return _instance;
        }
    }

    void Start()
    {
        Paused = false;
    }
}