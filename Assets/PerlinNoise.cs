using System.Collections;
using UnityEngine;

public static class PerlinNoise {

    public static float [,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        // "Vector2 offset" input allows us to scroll through the noise at our own value.

        float[,] noiseMap = new float[mapWidth, mapHeight];  // Creates a new 2-dimensional array.  Each item (pixels in our case) has an x and y coord.

        // "prng" = pseudo random number generator.
        System.Random prng = new System.Random (seed);

        // We wante each octave to be sample from a random location.
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            // We don't want to give Mathf.PerlinNoise a value that's too high, or else we get the same value over and over again.
            // So a range of -100000 to 100000 works pretty well.
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        
        if (scale <= 0)  // Just in case we get a value of 0 and end up dividing by 0.  This will prevent divide by zero errors popping up.
        {
            scale = 0.0001f;
        }

        // To keep track of our lowest and highest values of our noise map.
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        // It would be nice if when we scaled it scaled to the center of the map instead of to the top right corner.
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y=0; y < mapHeight; y++)  // Loop through the x and y creating a value for x and y at each pixel.
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;  // Keeps track of our current height value.

                // A loop through all of our octaves. a.k.a -> this is our octaves loop.
                for (int i = 0; i < octaves; i++)
                {
                    // This will make the values a percentage or a value between 0 and 1.
                    // This takes into account our frequency.  The higher the frequency, the further apart our sample points will be
                    // which will mean that the height values will change more rapidly.
                    // Subtracting the x and y values by half makes sure we scale to the center of the map instead of the top right corner.
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;
                    
                    // By default the value we get out of PerlinNoise is 0 to 1, but to get more interesting noise it would be nice
                    // if it could be negative so that our noise height would decrease.
                    // Multiplying by 2 and subtracting 1 allows us to be in the negative range of -1 to 1.
                    // This is confusing but take for example the value of 0.2.  If you take 0.2*2 you get 0.4.  Subtract 1 and you get -0.6 so it works out.
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    // Instead of setting teh noiseMap equal to the perlinValue, we'll increase the noise height by the perlin value of each octave.
                    noiseHeight += perlinValue * amplitude;

                    // At the end of each octave, the amplitude get's multiplied by the persistance value.
                    // Remember this is in the range 0 to 1 so it decreases each octave.
                    amplitude *= persistance;

                    // Frequency gets multiplied by lacunarity so the frequency increases each octave since lacunarity should be greater than 1.
                    frequency *= lacunarity;
                }
                // After we've finished generating the octaves.
                // Allows us to know what range our noise values are in.
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight){
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;

            }
        }

        // Now that we know our noise values range, for each value in the noise map
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // Mathf.InverseLerp returns a value between 0 and 1.  We've effectively normalized our noiseMap x and y values.
                // Now we can return it.
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
                return noiseMap;
    }
}
