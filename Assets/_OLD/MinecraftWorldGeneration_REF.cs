using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecraftWorldGeneration_REF : MonoBehaviour {
	
	public static int width = 128;
	public static int height = 128;
	public static int depth = 128;
	public float detailScale = 25.0f;
	public int diamondPerc = 40;  // "Use this to set the amount of diamonds which we'll use later on."
	public int heightOffset = 100; // it's 128 because our height is 128.  So we'll have 28 blocks above 0.
	public int heightScale = 20;

	public GameObject grassPrefab;

	void Start () {
		int seed = (int)Network.time * 10;
		for (int z = 0; z < depth; z++) {
			for (int x = 0; x < width; x++) {
				int y = (int)(Mathf.PerlinNoise ((x + seed) / detailScale, (z + seed) / detailScale) * heightScale) + heightOffset;
				Vector3 blockPos = new Vector3 (x, y, z);

				CreateBlock (y, blockPos, true); //we are going to create this as it's the first block to be created.

				//	We are starting to create blocks at the height offset.  So when we run this loop
				// 	we want to decrement by 1 (y--) until we reach 0;
				while (y > 0) {
					y--;
					blockPos = new Vector3 (x, y, z);
					CreateBlock (y, blockPos, false); //This will create every block in the game so we want to not instantiate it yet, hence "false".
				}
			}
		}
	}
	
	void CreateBlock(int y, Vector3 blockPos, bool create){
		if (y > 115) {
			if (create) {
				Instantiate (grassPrefab, blockPos, Quaternion.identity);
			}
		} else {
			if (create) {
				Instantiate (grassPrefab, blockPos, Quaternion.identity);
			}
		}
	}
}
