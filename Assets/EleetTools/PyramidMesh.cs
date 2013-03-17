using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PyramidType
{
    Tetrahedron,
    SquarePyramid
}

[Serializable]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class PyramidMesh : MonoBehaviour
{
    #region properties

    [SerializeField]
    [HideInInspector]
    private PyramidType _type;
    public PyramidType Type
    {
        get { return _type; }
        set
        {
            PyramidType old = _type;
            _type = value;
            if (_type != old)
                GenerateMesh();
        }
    }

    [SerializeField]
    [HideInInspector]
    private float _width = 1f;
    public float Width
    {
        get { return _width; }
        set
        {
            float old = _width;
            _width = value;
            if (_width != old)
                GenerateMesh();
        }
    }

    [SerializeField]
    [HideInInspector]
    private float _height = 1f;
    public float Height
    {
        get { return _height; }
        set
        {
            float old = _height;
            _height = value;
            if (_height != old)
                GenerateMesh();
        }
    }

    [SerializeField]
    [HideInInspector]
    private float _depth = 1f;
    public float Depth
    {
        get { return _depth; }
        set
        {
            float old = _depth;
            _depth = value;
            if (_depth != old)
                GenerateMesh();
        }
    }

    private Mesh _mesh;
    public Mesh Mesh
    {
        get
        {
            if (_mesh == null)
            {
                _mesh = new Mesh();
                GetComponent<MeshFilter>().sharedMesh = _mesh;
            }
            return _mesh;
        }
    }

    #endregion properties

    void Start()
    {
        GenerateMesh();
    }

    #region helper functions

    Vector3[] GetVerticies()
    {
        float halfWidth = Width / 2f;
        float halfHeight = Height / 2f;
        float halfDepth = Depth / 2f;

        Vector3 zero = new Vector3(0, halfHeight, 0);
        Vector3 one, two, three, four;
        switch (Type)
        {
            case PyramidType.Tetrahedron:
                one = new Vector3(-halfWidth, -halfHeight, halfDepth);
                two = new Vector3(halfWidth, -halfHeight, halfDepth);
                three = new Vector3(0, -halfHeight, -halfDepth);

                return new Vector3[] {
                    // Front
                    zero, one, two,
                    // Left
                    zero, three, one,
                    // Right
                    zero, two, three,
                    // Bottom
                    one, three, two
                };
            case PyramidType.SquarePyramid:
                one = new Vector3(-halfWidth, -halfHeight, halfDepth);
                two = new Vector3(halfWidth, -halfHeight, halfDepth);
                three = new Vector3(-halfWidth, -halfHeight, -halfDepth);
                four = new Vector3(halfWidth, -halfHeight, -halfDepth);

                return new Vector3[] {
                    // First face
                    zero, one, two,
                    // Left face
                    zero, three, one,
                    // Back face
                    zero, four, three,
                    // Right face
                    zero, two, four,
                    // bottom
                    one, three, four, two
                };
        }

        return new Vector3[0];
    }

    int[] GetTriangles()
    {
        switch (Type)
        {
            case PyramidType.Tetrahedron:
                return new int[] {
                    // Front
                    0, 1, 2,
                    // Left
                    3, 4, 5,
                    // Right
                    6, 7, 8,
                    // Back
                    9, 10, 11
                };
            case PyramidType.SquarePyramid:
                return new int[] { 
                    // Front
                    0, 1, 2,
                    // Left
                    3, 4, 5,
                    // Back
                    6, 7, 8,
                    // Right
                    9, 10, 11,
                    // Bottom
                    12, 13, 15,
                    13, 14, 15
                };
        }

        return new int[0];
    }

    Vector2[] GetUVCoords()
    {
        Vector2 uvOne = new Vector2(0.5f, 1f);
        Vector2 uvTwo = new Vector2(0f, 0f);
        Vector2 uvThree = new Vector2(1f, 0f);

        switch (Type)
        {
            case PyramidType.Tetrahedron:
                return new Vector2[] {
                    // Front
                    uvOne, uvTwo, uvThree,
                    // Left
                    uvOne, uvTwo, uvThree,
                    // Right
                    uvOne, uvTwo, uvThree,
                    // Bottom
                    uvOne, uvTwo, uvThree
                };
            case PyramidType.SquarePyramid:
                return new Vector2[] {
                    // Front
                    uvOne, uvTwo, uvThree,
                    // Left
                    uvOne, uvTwo, uvThree,
                    // Back
                    uvOne, uvTwo, uvThree,
                    // Right
                    uvOne, uvTwo, uvThree,
                    // Bottom
                    new Vector2(0f, 1f),
                    uvTwo,
                    uvThree,
                    new Vector2(1f, 1f)
                };
        }

        return new Vector2[0];
    }

    #endregion helper functions

    #region public functions

    [ContextMenu("Generate Mesh")]
    public void GenerateMesh()
    {
        Material mat = GetComponent<MeshRenderer>().sharedMaterial;
        if (mat == null)
        {
            mat = new Material(Shader.Find("Diffuse"));
            GetComponent<MeshRenderer>().sharedMaterial = mat;
        }

        _mesh = null;
        Mesh.vertices = GetVerticies();
        Mesh.triangles = GetTriangles();
        Mesh.uv = GetUVCoords();
        Mesh.RecalculateNormals();
    }

    #endregion public functions
}