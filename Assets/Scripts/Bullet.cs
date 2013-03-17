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

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("player"))
            Destroy(gameObject);
    }

    public void Fire()
    {
        if (fired)
            return;
        fired = true;
        rigidbody.AddForce(direction * initialForce);
    }
}