using System.Collections;
using UnityEngine;

public static class PerlinNoise {

    public static float [,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];  // Creates a new 2-dimensional array.  Each item (pixels in our case) has an x and y coord.

        if (scale <= 0)  // Just in case we get a value of 0 and end up dividing by 0.  This will prevent divide by zero errors popping up.
        {
            scale = 0.0001f;
        }

        for (int y=0; y < mapHeight; y++)  // Loop through the x and y creating a value for x and y at each pixel.
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float sampleX = x / scale; // This will make the values a percentage or a value between 0 and 1.
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }
        return noiseMap;
    }
}
