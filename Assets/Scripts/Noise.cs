using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

    public static float [,] GenerateNoiseMap (int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        float [,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            // Throught testing we learned that we don't want to give Perlin Noise a value that's too high or it will just keep returning the same
            // value over and over again.
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;  //This makes sure we are never dividing by zero.
        }

        // Before we return the noiseMap data we need to make sure we normalize the values between 0 and 1 first.  So we'll add the following:
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        // This will fix so when we scale the map (zoom) it will zoom into the the center of the map instead of the top right corner.
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x; // The higher the frequency the further apart the sample points will be.  SO the height points will
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y; // change more rapidly.

                    // By default, the value of the perlinNoise is between 0 and 1.  But to make it more interesting we would like it to sometimes be negative
                    // so our noise height will decrease.  So to make it a negative value we'll mulitply by 2 and subtract 1.
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }


                // Referencing the note above about normalizing the values before returning the map data:
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        // Now that we know what our noiseMap values are, we'll want to loop through those noiseMap values again to normalize.
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //Inverse Lerp method returns a value between 0 and 1.
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }

}
