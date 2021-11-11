using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_TerrainGenerator : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    
    private Vector3[] _points;
    private int[] _triangles;

    [SerializeField] private int Width = 0;
    [SerializeField] private int Depth = 0;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        GeneratePoints(Width, Depth);
        GenerateTriangles(Width, Depth);

        FillMesh();
    }

    void FillMesh()
    {
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.vertices = _points;
        meshFilter.mesh.triangles = _triangles;
    }

    void GeneratePoints(int xMax, int zMax)
    {
        _points = new Vector3[xMax * zMax];

        for (int z = 0; z < zMax; z++)
        {
            for (int x = 0; x < xMax; x++)
            {
                Vector3 v = new Vector3(x, 0, z);
                _points[x + xMax * z] = v;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_points != null && _points.Length > 0)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                Gizmos.DrawSphere(_points[i], 0.5f);
            }
        }
    }

    void GenerateTriangles(int xMax, int zMax)
    {
        _triangles = new int[((xMax - 1) * (zMax - 1)) * 6];

        int index = 0;

        for (int z = 0; z < zMax - 1; z++)
        {
            for (int x = 0; x < xMax - 1; x++)
            {
                int upperRange = xMax;
                int position = x + z * xMax;

                _triangles[index] = position;
                _triangles[index + 1] = position + upperRange;
                _triangles[index + 2] = position + upperRange + 1;
                _triangles[index + 3] = position;
                _triangles[index + 4] = position + upperRange + 1;
                _triangles[index + 5] = position + 1;



                index += 6;
            }
        }
    }

}
