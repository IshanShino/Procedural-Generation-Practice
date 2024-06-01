using UnityEngine;
public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int seed, int octaves, float persistence, 
    float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed); // generate randomized maps on different seeds
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float minNoiseHeight = float.MaxValue;
        float maxNoiseHeight = float.MinValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0 ; x < mapWidth; x++)
            {   
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0f;

                for (int i = 0; i < octaves; i++)
                {   
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleZ = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ) * 2 - 1;  // * 2 - 1 is done so that the perlin noise can sometimes be negative  
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;  // persistence < 1. amplitude deacreases each octave 
                    frequency *= lacunarity;  //  lacunarity < 1. amplitude deacreases each octave 
                }    

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }  
                if (noiseHeight < minNoiseHeight)    
                {
                    minNoiseHeight = noiseHeight;
                }    
                noiseMap[x, y] = noiseHeight;
            }
        }
         for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0 ; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]); // nomralizing the noiseMap so that all the values are between 0&1.
            }
        }  
        return noiseMap;
    }
}
