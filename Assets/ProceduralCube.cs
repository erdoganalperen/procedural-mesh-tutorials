using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour
{
    private Mesh _mesh;
    private List<Vector3> _vertices;
    private List<int> _triangles;

    public float scale = 1f;
    public int posX, posY, posZ;
    private float _adjScale;
    private void Awake()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _adjScale = scale * .5f;
    }

    private void Start()
    {
        MakeCube(_adjScale,new Vector3(posX*scale,posY*scale,posZ*scale));
        UpdateMesh();
    }

    private void MakeCube(float cubeScale,Vector3 cubePos)
    {
        _vertices = new List<Vector3>();
        _triangles = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            MakeFace(i,cubeScale,cubePos);
        }
    }
    
    private void MakeFace(int dir,float faceScale,Vector3 facePos)
    {
        _vertices.AddRange(CubeMeshData.faceVertices(dir,faceScale,facePos));
        int vCount = _vertices.Count;
        _triangles.Add(vCount-4);
        _triangles.Add(vCount-4+1);
        _triangles.Add(vCount-4+2);
        _triangles.Add(vCount-4);
        _triangles.Add(vCount-4+2);
        _triangles.Add(vCount-4+3);

    }

    private void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.RecalculateNormals();
    }

}