using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colorMap = new Color[width * height]; // this instance of the colorMap is the noiseMap

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {   
                // since the noiseMap is a 2D array we're mapping the indices of that into a 1D array in the colorMap
                // the formula for that is array[row * width + col]. i.e, recaclulation of 2D array indices into 1D array index.
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]); 
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }
}
