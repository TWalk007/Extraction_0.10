using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
/*Having the above makes sure when this script is dropped on an object it has these things it needs.  
 If not it automatically creates them.*/

public class TileMap : MonoBehaviour {
        
    
	void Start () {
        BuildMesh();

	}

    void BuildMesh()
    {
        Mesh mesh = new Mesh();


        /*
         * This is where we'll generate the mesh data.
         * We need an array to capture all of the Vector3 points.  Then we have to specify the size when we create it.
         * Note that we'll start with a single mesh which has 4 points.
         */
        Vector3[] vertices = new Vector3[4];

        // There are 2 triangles with 3 points each.  We need to capture the total vertices used for each triangle.
        int[] triangles = new int[ 2 * 3];

        /*
         * Collects the directional data for each vertices that we've collected above.  If we would like a surface that isn't smooth
         * then the we would need to create 2 normal directions at the same vertices locations where triangles share vertices.
        */
        Vector3[] normals = new Vector3[4];

        // Since we are making this smooth we only need 4 uv directions stored, one for each vertex.
        Vector2[] uv = new Vector2[4];

        // Now that they are created we are going to loop and initialize these.  Remember to use the z direction as y = height above ground.
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 0, -1);
        vertices[3] = new Vector3(1, 0, -1);

        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 1;
        triangles[5] = 3;

        normals[0] = Vector3.up;
        normals[1] = Vector3.up;
        normals[2] = Vector3.up;
        normals[3] = Vector3.up;

        // UV coordinates go from 0,0 - 1,1.  It's always between 0-1 and is a float.
        // If the bitmap comes out upside down then you don't have the uv's mapped to the right locations (same location as where the correct
        // vertices are.  Remember unity's uv mappes build from bottom up instead of top down which is how we're building the map above.
        uv[0] = new Vector2(0, 1); // top left of surface
        uv[1] = new Vector2(1, 1); // top right of surface
        uv[2] = new Vector2(0, 0); // bottom right of surface
        uv[3] = new Vector2(1, 0); // bottom left of surface

        /*
         * A mesh needs a series of information to be filled out. Every triangle has 3 points and the mesh is made of triangles.
         * So we need vertices, triangles, and normals (which direction the vertices are facing).  After generating these things we'll
         * assign them to the cached (placed in memory) holders below: vertices, triangles, and normals.
        */
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        

        // Assign our mesh to our filterer/renderer/collider. (We don't actually assign it to the renderer, but for now let's "pretend").
        MeshFilter _meshFilter = GetComponent<MeshFilter>();
        MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();
        MeshCollider _meshCollider = GetComponent<MeshCollider>();
        mesh.uv = uv;

        // We have to assign our mesh component to our meshfilter's mesh slot or else we won't see it.
        _meshFilter.mesh = mesh;
        _meshCollider.sharedMesh = mesh;
    }

}
