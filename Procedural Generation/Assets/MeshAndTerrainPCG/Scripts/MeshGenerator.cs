using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{   
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;

    public int xSize = 30; // x = columns
    public int zSize = 30; // z = rows
    public float amplitude = 2f;

    private void Awake() 
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    private void Update()
    {   
        CreateMesh();
        UpdateMesh();
    }
    private void CreateMesh()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];   // total vertex count
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {   
                float y = Mathf.Sin(x + z + Time.time) * amplitude;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int verts = 0; // moving to the next vertex every iteration
        int tris = 0; // moving to the next triangle every iteration

        for (int z = 0; z < xSize; z++)
        {   
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] =  verts + 0;
                triangles[tris + 1] =  verts + xSize + 1;
                triangles[tris + 2] =  verts + 1;
                triangles[tris + 3] =  verts + xSize + 1;
                triangles[tris + 4] =  verts + xSize + 2;
                triangles[tris + 5] =  verts + 1;

                verts++;
                tris += 6;
            }
            verts++;
        }
    }

    private void UpdateMesh()
    {   
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
