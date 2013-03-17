using UnityEngine;
using System;
using System.Collections;

[Serializable]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class SquarePlaneMesh : MonoBehaviour {
    public float width = 1f;
    public float height = 1f;
	
	[SerializeField]
    [HideInInspector]
	private UVCoordinates _uvCoords;
	public UVCoordinates UVCoords { 
		get {
			if (_uvCoords == null) 
				_uvCoords = new UVCoordinates();
			return _uvCoords;
		}
		set {
			_uvCoords = value;
		}
	}
	
    private float _width = 1f;
    private float _height = 1f;
	
	[ContextMenu("Enable")]
	void OnEnable() {
		UVCoords = new UVCoordinates();
	    UVCoords.TopLeft = new Vector2(0, 1);
	    UVCoords.BottomLeft = new Vector2(0, 0);
	    UVCoords.BottomRight = new Vector2(1, 0);
	    UVCoords.TopRight = new Vector2(1, 1);
        GetComponent<MeshFilter>().sharedMesh = new Mesh();
	}
	
    void Update() {
        if (_width != width || _height != height) {
            _width = width;
            _height = height;
            GenerateMesh();
        }
    }
	
	#region public functions
	
	public void SetSize(float width, float height) {
		this.width = width;
		this.height = height;
	}
	
    [ContextMenu("Generate Mesh")]
    public void GenerateMesh() {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
		Mesh mesh = meshFilter.sharedMesh;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = renderer.sharedMaterial;
        if (!material) {
            material = new Material(Shader.Find("Diffuse"));
            renderer.sharedMaterial = material;
        }
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[] { 0, 1, 3, 3, 1, 2 };
		
        float halfWidth = width / 2.0f;
        float halfHeight = height / 2.0f;
		
		// Clockwise gives texture facing -Z
        vertices[0] = new Vector3(-halfWidth, -halfHeight, 0);
        vertices[1] = new Vector3(-halfWidth, halfHeight, 0);
        vertices[2] = new Vector3(halfWidth, halfHeight, 0);
        vertices[3] = new Vector3(halfWidth, -halfHeight, 0);

        mesh.vertices = vertices;
        mesh.uv = UVCoords.ToArray();
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.Optimize();
    }
	
	#endregion public functions
}
