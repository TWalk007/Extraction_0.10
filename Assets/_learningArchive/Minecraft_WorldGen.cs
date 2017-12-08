using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecraft_WorldGen : MonoBehaviour {
    public static int width = 128;
    public static int height = 128;
    public static int depth = 128;
    public float detailScale = 25.0f;
    public int diamondPerc = 40;
    public int heightOffset = 128;
	public int heightScale = 20;

    public GameObject dirtPrefab;
	public GameObject stonePrefab;

    private void Start()
    {
        int seed = (int)Network.time * 10;
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
				int y = (int)(Mathf.PerlinNoise((x+seed) / detailScale, (z + seed) / detailScale) * heightScale) + heightOffset;
				Vector3 blockPos = new Vector3 (x, y, z);
				CreateBlock (y, blockPos, true);
				while (y > 0) {
					y--;
					blockPos = new Vector3 (x, y, z);
					CreateBlock (y, blockPos, false);
				}
            }
        }
    }

	private void CreateBlock (int y, Vector3 blockPos, bool create)
	{
		if (y > 110)
		{
			if (create)
			{
				Instantiate (dirtPrefab, blockPos, Quaternion.identity);	
			}
		} else if (y <= 110)
		{
			if (create)
			{
				Instantiate (stonePrefab, blockPos, Quaternion.identity);	
			}
		}
	}
}
