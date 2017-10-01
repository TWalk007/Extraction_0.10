using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode] // Tells Unity to run this script in the editor.

[RequireComponent (typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]

public class TileMap : MonoBehaviour {

    public int size_x = 100;
    public int size_z = 50;
    public float tileSize = 1.0f;

	void Start () {
        BuildMesh();
	}

    void BuildMesh()
    {
        Mesh mesh = new Mesh();

        int numTiles = size_x * size_z;
        int numTris = numTiles * 2;  // There are twice as many triangles as there are quads (tiles).

        int vsize_x = size_x + 1;  // The vertices size is always one more than the tile size so we add 1.
        int vsize_z = size_z + 1;  // Same in the "z" direction as the x.
        int numVerts = vsize_x * vsize_z;

        Vector3[] vertices = new Vector3[ numVerts ];
        int[] triangles = new int[ numTris * 3];  // There are 3 vertices per triangle.
        Vector3[] normals = new Vector3[ numVerts ];
        Vector2[] uv = new Vector2[ numVerts ];

        for(int z=0; z < vsize_z; z++)  // We're using "vsize_" as we are creating the vertices first before the triangles.
        {
            for (int x = 0; x < vsize_x; x++)  // First we'll fill out the horizontal "X" direction, then move up a row  in the "z" direction.
            {  // Multiply by "z" will specify the row it's on.  Add "x" to the vsize_x will keep track of the horizontal place.
               // Remember the brackets below specify the "index" of the array.  Where to place the information generated.
                vertices [z * vsize_x + x] = new Vector3(x * tileSize, 0, z * tileSize);
                normals [z * vsize_x + x] = Vector3.up;

                // When x=0, we want our uv.x= 0.
                // When x = 101, we want our uv.x = 1.
                // So we can say uv.x = x / vsize_x. (gives a percentage or a float value less than 1)
                // Now this will give us an integer so we need to convert it to a float.
                uv[z * vsize_x + x] = new Vector2( (float) x / size_x, (float) z / size_z );
            }
        }

        for (int z = 0; z < size_z; z++)  // We changed it to be the actual size, not the vertices size for triangle generation.
        {
            for (int x = 0; x < size_x; x++)
            {
                // For each one of these we're going to create a pair of triangles.
                // Z and X are defining a quad, so we'll need to index the quad as we generate.
                int quadIndex = z * size_x + x;  // Just like in the above loops it is keeping track of the array index this way.
                int triOffset = quadIndex * 6; // There are 3 points per triangle and 2 triangles in a quad.  so 3 * 2 = 6.


                // This is 0, 1, 0 as those are the coordinate to build our triangle... we'll say triangle "A"
                // We need to have an offset to make sure we're making the correct triangle so we use the same as above:
                // "z * vsize_x + x" before we tell it the shape with  the rest of the equation.
                // Remember the left brackets tell the array where to place the info, we still need the place on the right side to grab the correct vertices.
                triangles[triOffset + 0] = z * vsize_x + x + 0;
                triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
                triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;

                triangles[triOffset + 3] = z * vsize_x + x +           0;
                triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
                triangles[triOffset + 5] = z * vsize_x + x +           1; 
            }
        }



        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        
        MeshFilter _meshFilter = GetComponent<MeshFilter>();
        MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();
        MeshCollider _meshCollider = GetComponent<MeshCollider>();
        mesh.uv = uv;

        _meshFilter.mesh = mesh;
        _meshCollider.sharedMesh = mesh;
    }

}
