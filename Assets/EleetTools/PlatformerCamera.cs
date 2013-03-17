using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class PlatformerCamera : MonoBehaviour 
{
    public Transform followTarget;
    public float verticalLeeway = 0.04f;
    public float horizontalLeeway = 0.05f;
    public Vector3 offset = Vector3.zero;

    Vector3 previousLocation;

    void Start()
    {
        previousLocation = followTarget.position;
        UpdatePosition();
        transform.position = followTarget.position + offset;
        transform.LookAt(followTarget);
		Quaternion rot = camera.transform.rotation;
		rot.x = 0;
		rot.y = 0;
		rot.z = 0;
		transform.rotation = rot;
    }
    
    [ExecuteInEditMode]
    void Update()
    {
        UpdatePosition();
    }

    #region helper functions

    void UpdatePosition()
    {
        if (followTarget == null)
            return;
        Vector3 topRight = camera.WorldToViewportPoint(followTarget.position + followTarget.collider.bounds.extents);
        Vector3 bottomLeft = camera.WorldToViewportPoint(followTarget.position - followTarget.collider.bounds.extents);
        Vector3 pos = transform.position;
        if (topRight.x > 0.5f + verticalLeeway || bottomLeft.x < 0.5f - verticalLeeway)
        {
            float xDiff = followTarget.position.x - previousLocation.x;
            pos.x += xDiff;
        }
        if (topRight.y > 0.5f + horizontalLeeway || bottomLeft.y < 0.5f - horizontalLeeway)
        {
            float yDiff = followTarget.position.y - previousLocation.y;
            pos.y += yDiff;
        }
        transform.position = pos;

        previousLocation = followTarget.position;
    }

    #endregion helper functions
}
