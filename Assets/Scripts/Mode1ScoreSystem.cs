using UnityEngine;
using System;
using System.Collections;

public class Mode1ScoreSystem : MonoBehaviour 
{
    public delegate void ScoreChangeHandler(int newScore);
    public event ScoreChangeHandler ScoreChanged = delegate { };
    
    public int Score { get; private set; }

    private float multiplier = 1f;
    private int chained = 0;

    void Start()
    {
        Score = 0;
    }

    #region public functions

    public void AddScore(int value)
    {
        Score += Mathf.RoundToInt(value * multiplier);
        if (Score < 0)
            Score = 0;
        ScoreChanged(Score);
        chained += 1;
        if (chained >= 5)
        {
            multiplier += 0.1f;
            chained = 0;
        }
    }

    public void RemoveScore(int value)
    {
        Score -= value;
        if (Score < 0)
            Score = 0;
        ScoreChanged(Score);
        multiplier = 1f;
        chained = 0;
    }

    #endregion public functions
}
