using UnityEngine;
using UnityRandom = UnityEngine.Random;
using System;
using System.Collections;

public class CameraShake : MonoBehaviour 
{
    public float minX = 0.1f;
    public float maxX = 0.5f;
    public float minY = 0.1f;
    public float maxY = 0.5f;
    public float minZ = 0.1f;
    public float maxZ = 0.5f;
    public float defaultShakeDuration = 1f;

    private bool shaking = false;
    private float shakeEnd;

    private Vector3 returnPosition;

    void Update()
    {
        if (shaking)
        {
            if (Time.time > shakeEnd)
            {
                shaking = false;
                transform.localPosition = returnPosition;
            }
            else
            {
                Vector3 shift = Vector3.zero;
                shift.x = UnityRandom.Range(minX, maxX);
                shift.y = UnityRandom.Range(minY, maxY);
                shift.z = UnityRandom.Range(minZ, maxZ);
                transform.localPosition = returnPosition + shift;
            }
        }
    }

    #region public functions

    public void Shake()
    {
        Shake(defaultShakeDuration);
    }

    public void Shake(float duration)
    {
        shakeEnd = Time.time + duration;
        shaking = true;
        returnPosition = transform.localPosition;
    }

    public void ShiftReturnPosition(Vector3 shift)
    {
        if (shaking)
            returnPosition += shift;
    }

    #endregion public functions
}
