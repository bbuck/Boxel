using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Bullet : MonoBehaviour 
{
    public float initialForce = 100f;
    public Vector3 direction = Vector3.forward;

    private bool fired = false;

    private static Mode1ScoreSystem _scoreSystem;
    private static Mode1ScoreSystem ScoreSystem
    {
        get
        {
            if (_scoreSystem == null)
                _scoreSystem = GameObject.Find("global").GetComponent<Mode1ScoreSystem>();
            return _scoreSystem;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("player"))
        {
            if (other.CompareTag("enemy"))
            {
                Mode1Enemy enemy = other.GetComponent<Mode1Enemy>();
                ScoreSystem.AddScore(enemy.scoreValue);
                enemy.Destroy();
            }
            Destroy(gameObject);
        }
    }

    #region public functions

    public void Fire()
    {
        if (fired)
            return;
        fired = true;
        rigidbody.AddForce(direction * initialForce);
    }

    #endregion public functions
}