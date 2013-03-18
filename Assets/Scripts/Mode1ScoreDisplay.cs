using UnityEngine;
using System;
using System.Collections;

public class Mode1ScoreDisplay : MonoBehaviour 
{
    public TextMesh display;
    public float growSize = 0.2f;
    public float shrinkSpeed = 1.5f;

    private int current = 0;
    private int changeTo = -1;

    private bool shrinking = false;
    private float targetCharSize;

    private Mode1ScoreSystem scoreSystem;

    void OnEnable()
    {
        scoreSystem = GameObject.Find("global").GetComponent<Mode1ScoreSystem>();
        scoreSystem.ScoreChanged += ScoreChanged;
        targetCharSize = display.characterSize;
    }

    void OnDisable()
    {
        scoreSystem.ScoreChanged -= ScoreChanged;
    }

    void Update()
    {
        if (shrinking)
        {
            float charSize = Mathf.Lerp(display.characterSize, targetCharSize, Time.deltaTime * shrinkSpeed);
            if (charSize < targetCharSize)
            {
                charSize = targetCharSize;
                shrinking = false;
            }
            display.characterSize = charSize;
        }
        
        if (changeTo >= 0 && changeTo != current)
        {
            int incr = changeTo > current ? 1 : -1;
            current += incr;
            display.text = current.ToString("G0");
        }
    }

    #region helper functions

    void ScoreChanged(int newScore)
    {
        changeTo = newScore;
        if (changeTo > current)
        {
            shrinking = true;
            display.characterSize += growSize;
        }
    }

    #endregion public functions
}
