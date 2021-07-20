using System;
using UnityEngine;

namespace GridMesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ProceduralGrid : MonoBehaviour
    {
        private Mesh _mesh;
        private Vector3[] _vertices;

        private int[] _triangles;

//grid settings
        public float cellSize = 1;
        public Vector3 gridOffset;
        public int gridSize;

        private void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
        }

        private void Start()
        {
            MakeDiscreteProceduralGrid();
            UpdateMesh();
        }

        private void UpdateMesh()
        {
            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();
        }

        private void MakeDiscreteProceduralGrid()
        {
            //set array sizes
            _vertices = new Vector3[4];
            _triangles = new int[6];
            //set vertex offsets
            float vertexOffset = cellSize * .5f;
            //populate the vertices and triangles array
            _vertices[0] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffset;
            _vertices[1] = new Vector3(-vertexOffset, 0, vertexOffset) + gridOffset;
            _vertices[2] = new Vector3(vertexOffset, 0, -vertexOffset) + gridOffset;
            _vertices[3] = new Vector3(vertexOffset, 0, vertexOffset) + gridOffset;
            _triangles[0] = 0;
            _triangles[1] = 1;
            _triangles[2] = _triangles[3] = 2;
            _triangles[4] = 1;
            _triangles[5] = 3;
        }
    }
}