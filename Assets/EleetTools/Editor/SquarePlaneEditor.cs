using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SquarePlaneMesh))]
public class SquarePlaneEditor : Editor {
	private bool showUvCoords = false;
	
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

        SquarePlaneMesh plane = target as SquarePlaneMesh;
		
		showUvCoords = EditorGUILayout.Foldout(showUvCoords, "UV Coordinates");
		if (showUvCoords) {
			if (plane.UVCoords == null)
				plane.UVCoords = new UVCoordinates();
			plane.UVCoords.Flip = EditorGUILayout.Toggle("Flip", plane.UVCoords.Flip);
			plane.UVCoords.TopLeft = EditorGUILayout.Vector2Field("Top Left", plane.UVCoords.TopLeft);
			plane.UVCoords.BottomLeft = EditorGUILayout.Vector2Field("Bottom Left", plane.UVCoords.BottomLeft);
			plane.UVCoords.BottomRight = EditorGUILayout.Vector2Field("Bottom Right", plane.UVCoords.BottomRight);
			plane.UVCoords.TopRight = EditorGUILayout.Vector2Field("Top Right", plane.UVCoords.TopRight);
		}
		
		if (GUILayout.Button("Generate Mesh")) {
			plane.GenerateMesh();	
		}
		
		Repaint();
	}
}
