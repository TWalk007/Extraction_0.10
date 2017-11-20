using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator {

    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
        {
            Texture2D texture = new Texture2D (width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels (colorMap);
            texture.Apply();
            return texture;
        }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
        {
            int width = heightMap.GetLength(0); //This is getting the value of the first dimension of the array.
            int height = heightMap.GetLength(1); // This is getting the value of the second dimension of the array.

            //We'll set the color of the pixels of the texture.
            // It's faster to generate an array of all the colors for all the pixels and then just set them all at once.
            Color[] colorMap = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Get the index of the 2Dim array below first by doing [y * width].  The index of the "row" were currently on.
                    // To get the column, we'll just add the "x" value.
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                }
            }
            return TextureFromColorMap (colorMap, width, height);
        }

}
