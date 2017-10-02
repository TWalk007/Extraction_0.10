using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (TileMap))]
public class TileMapMouse : MonoBehaviour {

    public Transform selectionCube;

    Vector3 currentTileCoord;
    TileMap _tileMap;

    void Start()
    {
        _tileMap = GetComponent<TileMap>();
        selectionCube.transform.localScale = selectionCube.transform.localScale * _tileMap.tileSize;
    }

    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;  // This tells us the exact coordinates of the mouse position (more specific than if the mouse is hitting the collider or not).

        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            int x = Mathf.FloorToInt( hitInfo.point.x / _tileMap.tileSize);  
            int z = Mathf.FloorToInt( hitInfo.point.z / _tileMap.tileSize);
            //Debug.Log("Tile: " + x + ", " + z);

            currentTileCoord.x = x;
            currentTileCoord.z = z;
                        
            selectionCube.transform.position = currentTileCoord * _tileMap.tileSize;
        }
        else
        {
            // Hide selection cube?
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Click!");
        }

    }
}
