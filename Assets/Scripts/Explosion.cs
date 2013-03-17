using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour 
{
    public float lifetime = 0.2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}