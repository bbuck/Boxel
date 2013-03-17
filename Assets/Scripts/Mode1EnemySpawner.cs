using UnityEngine;
using UnityRandom = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mode1EnemySpawner : MonoBehaviour 
{
    private const int Top = 0,
                      Left = 1,
                      Bottom = 2,
                      Right = 3;
    
    public float spawnDelay = 0.5f;
    public float defaultY = 0.3299553f;
    public float spawnForce = 500f;
    public bool Spawning { get; set; }

    private float nextSpawn = 0f;

    public static Mode1EnemySpawner MobSpawner { get; private set; }

    void Start()
    {
        MobSpawner = this;
        Spawning = true;
    }

    void Update()
    {
        if (Spawning && Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnDelay;
            // 0 - Top, 1 - Left, 2 - Bottom, 3 - Right
            int direction = UnityRandom.Range(0, 3);
            float x, y;
            x = y = 0f;
            Vector3 forceDirection = Vector3.zero;
            switch (direction)
            {
                case Top:
                    y = 1.1f;
                    x = UnityRandom.Range(0.1f, 0.9f);
                    forceDirection = Vector3.back;
                    break;
                case Left:
                    x = -0.1f;
                    y = UnityRandom.Range(0.1f, 0.9f);
                    forceDirection = Vector3.right;
                    break;
                case Right:
                    x = 1.1f;
                    y = UnityRandom.Range(0.1f, 0.9f);
                    forceDirection = Vector3.left;
                    break;
                case Bottom:
                    x = UnityRandom.Range(0.1f, 0.9f);
                    y = -0.1f;
                    forceDirection = Vector3.forward;
                    break;
            }

            Vector3 spawnLocation = new Vector3(x, y, 0);
            spawnLocation = Camera.main.ViewportToWorldPoint(spawnLocation);
            spawnLocation.y = defaultY;
            GameObject enemyObject = PrefabManager.Instance.CreateInstance("Enemy", spawnLocation);
            enemyObject.rigidbody.AddForce(forceDirection * spawnForce);
        }
    }
}