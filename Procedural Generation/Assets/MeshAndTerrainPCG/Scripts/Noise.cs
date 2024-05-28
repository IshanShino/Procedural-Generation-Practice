using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int z = 0; z < mapHeight; z++)
        {
            for (int x = 0 ; x < mapWidth; x++)
            {   
                  
                float sampleX = x / scale;
                float sampleY = z / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, z] = perlinValue;
                
            }
        }
        return noiseMap;
    }
}
