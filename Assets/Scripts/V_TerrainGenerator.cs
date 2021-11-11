using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int depth;
    [SerializeField] private float pointSpacing;
    
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Mesh _mesh;

    private Vector3[] vertices;
    private int[] triangles;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrain();
        }
    }

    private void GenerateTerrain()
    {
        vertices = new Vector3[width * depth];
        triangles = new int[((width-1) * (depth-1)) * 6];
        
        int index = 0;
        for (int j = 0; j < depth; j++)
        {
            for (int i = 0; i < width; i++)
            {
                vertices[index] = new Vector3(i * pointSpacing, 0, j * pointSpacing);
                index++;
            }
        }

        int tris = 0;
        for (int j = 0; j < depth - 1; j++)
        {
            for (int i = 0; i < width - 1; i++)
            {
                triangles[tris] = (i + j * width);
                triangles[tris + 1] = (i + j * width) + width;
                triangles[tris + 2] = (i + j * width) + width + 1;
                
                triangles[tris + 3] = (i + j * width);
                triangles[tris + 4] = (i + j * width) + width + 1;
                triangles[tris + 5] = (i + j * width) + 1;
                tris += 6;
            }
        }
        
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if (vertices != null && vertices.Length > 0)
        {
            foreach (Vector3 vertex in vertices)
            {
                Gizmos.DrawSphere(vertex, 0.25f);
            }
        }
    }
}