using UnityEngine;
using System;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
    private static PauseManager _instance;
    public static PauseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.name = "pause_manager";
                _instance = go.AddComponent<PauseManager>();
            }
            return _instance;
        }
    }

    public bool IsPaused { get; private set; }

    void OnEnable()
    {
        if (_instance == null)
            _instance = this;
        IsPaused = false;
    }

    void OnDisable()
    {
        Destroy(gameObject);
    }

    #region public functions

    public void ChangeState()
    {
        if (IsPaused)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (IsPaused)
            return;

        IsPaused = true;
        GameObject[] objects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in objects)
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
    }

    public void UnPause()
    {
        if (!IsPaused)
            return;

        IsPaused = false;
        GameObject[] objects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
    }

    #endregion public functions
}
