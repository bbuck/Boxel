using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mode1Enemy : MonoBehaviour
{
    void Start()
    {
        Invoke("AddOffscreenDestroy", 0.4f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player") || collision.gameObject.CompareTag("enemy"))
            Destroy();
    }

    #region helper functions

    void AddOffscreenDestroy()
    {
        gameObject.AddComponent<DestroyOffscreen>();
    }

    #endregion helper functions

    #region public functions

    public void Destroy()
    {
        PrefabManager.Instance.CreateInstance("Enemy Explosion", transform.position);
        Destroy(gameObject);
    }

    #endregion public functions
}