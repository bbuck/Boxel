using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DestroyOffscreen : MonoBehaviour 
{
    public Camera cameraObject;
    public bool testBounds = true;

    void Start()
    {
        if (cameraObject == null)
            cameraObject = Camera.main;
    }

    void Update()
    {
        if (testBounds)
            CheckBounds();
    }

    #region helper functions

    void CheckBounds()
    {
        Vector3 topRight = cameraObject.WorldToViewportPoint(transform.position + collider.bounds.extents);
        Vector3 bottomLeft = cameraObject.WorldToViewportPoint(transform.position - collider.bounds.extents);

        if (bottomLeft.x > 1 || bottomLeft.y > 1 || topRight.x < 0 || topRight.y < 0)
            Destroy(gameObject);
    }

    #endregion helper functions
}