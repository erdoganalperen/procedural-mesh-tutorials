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
            // MakeContiguousProceduralGrid();
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
            _vertices = new Vector3[gridSize * gridSize * 4];
            _triangles = new int[gridSize * gridSize * 6];
            //set vertex offsets
            //set tracker integers
            int v = 0;
            int t = 0;
            float vertexOffset = cellSize * .5f;
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);
                    //populate the vertices and triangles array
                    _vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                    _vertices[v + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                    _vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                    _vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                    _triangles[t] = v;
                    _triangles[t + 1] = _triangles[t + 4] = v + 1;
                    _triangles[t + 2] = _triangles[t + 3] = v + 2;
                    _triangles[t + 5] = v + 3;
                    v += 4;
                    t += 6;
                }
            }
        }
        private void MakeContiguousProceduralGrid()
        {
            //set array sizes
            _vertices = new Vector3[(gridSize + 1) * (gridSize+1)];
            _triangles = new int[gridSize * gridSize * 6];
            //set vertex offsets
            //set tracker integers
            int v = 0;
            int t = 0;
            float vertexOffset = cellSize * .5f;
            //create vertex grid
            for (int x = 0; x <= gridSize; x++)
            {
                for (int y = 0; y <= gridSize; y++)
                {
                    _vertices[v] = new Vector3((x * cellSize) - vertexOffset, 0, (y * cellSize) - vertexOffset);
                    v++;
                }
            }
            //reset vertex tracker
            v = 0;
            //setting each cell's triangles
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    _triangles[t] = v;
                    _triangles[t + 1] = _triangles[t + 4] = v + 1;
                    _triangles[t + 2] = _triangles[t + 3] = v + (gridSize+1);
                    _triangles[t + 5] = v + (gridSize+1) + 1;
                    v++;
                    t += 6;
                }
                v++;
            }
        }
    }
}