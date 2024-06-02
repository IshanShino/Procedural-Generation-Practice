using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{   
    public enum DrawMode {NoiseMap, ColorMap, Mesh}
    public DrawMode drawMode; 
    int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail; // for increasing the vertices by factor of 12
    public float noiseScale;
    public int seed;
    public int octaves;
    [Range(0, 1)] public float persistence;
    public float lacunarity;
    public Vector2 offset;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;
    
    public TerrainType[] regions;
    public bool autoUpdate;
    public void GenerateMap()
    {
        float[,] noiseMap =  Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, noiseScale, seed, octaves, persistence, lacunarity, offset); // since the plane is a square height = width = mapChunkSize
        
        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {   
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromNoiseMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
    }

    private void OnValidate() // called automatically whenever the script values are changes in the inspector
    {
        if (noiseScale < 0.3f)
        {
            noiseScale = 0.3f;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
    }

    [System.Serializable]
    public struct TerrainType
    {   
        public string name;
        public float height;
        public Color color;
    }
}
