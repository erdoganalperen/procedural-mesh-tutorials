using UnityEngine;

namespace OldMeshes
{
    [RequireComponent(typeof(MeshFilter))]
    public class ProceduralMesh6V : MonoBehaviour
    {
        private Mesh _mesh;
        private Vector3[] _vertices;
        private int[] _triangles;
        private void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
        }

        public void Update()
        {
            MakeMeshData();
            CreateMesh();
        }

        private void CreateMesh()
        {
            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();
        }

        private void MakeMeshData()
        {
            _vertices = new Vector3[]
            {
                new Vector3(0, YValue.Instance.yValue, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0),
                new Vector3(1,0,0),new Vector3(0,0,1),new Vector3(1,0,1)
            };
            _triangles = new int[] {0, 1, 2,3,4,5};
        }
    }
}