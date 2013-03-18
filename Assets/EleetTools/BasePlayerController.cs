using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterMotor))]
public class BasePlayerController : MonoBehaviour 
{
    public float speed = 1f;

    public bool LockPlayerControls { get; set; }
    public CharacterMotor Motor { get; private set; }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        if (LockPlayerControls)
        {
            CheckControls();
        }
        CheckNeverLockControls();
    }

    protected virtual void OnStart()
    {
        LockPlayerControls = false;
        Motor = GetComponent<CharacterMotor>();
    }

    protected virtual void CheckControls() { }
    protected virtual void CheckNeverLockControls() { }
}