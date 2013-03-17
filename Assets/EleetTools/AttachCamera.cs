using UnityEngine;
using System.Collections;

public enum Corner {
	TopRight,
	TopLeft,
	BottomRight,
	BottomLeft
}

[ExecuteInEditMode]
public class AttachCamera : MonoBehaviour {
	public Corner corner = Corner.TopRight;
	public Camera attachCamera;
	
	void OnEnable() {
		if (attachCamera == null)
			attachCamera = Camera.main;
	}
	
	void Update() {
		Vector3 screenPos = Vector3.zero;
		switch (corner) {
		case Corner.TopLeft:
			screenPos = attachCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
			break;
		case Corner.TopRight:
			screenPos = attachCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
			break;
		case Corner.BottomLeft:
			screenPos = attachCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
			break;
		case Corner.BottomRight:
			screenPos = attachCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));
			break;
		}
		screenPos.z = transform.position.z;
		transform.position = screenPos;
	}
}
