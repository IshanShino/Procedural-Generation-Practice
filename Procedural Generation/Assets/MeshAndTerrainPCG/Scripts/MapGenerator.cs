using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{   
    public enum DrawMode {NoiseMap, ColorMap, Mesh}
    public DrawMode drawMode; 
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int seed;
    public int octaves;
    [Range(0, 1)] public float persistence;
    public float lacunarity;
    public Vector2 offset;
    public TerrainType[] regions;

    public bool autoUpdate;
    public void GenerateMap()
    {
        float[,] noiseMap =  Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, seed, octaves, persistence, lacunarity, offset);
        
        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {   
                        colorMap[y * mapWidth + x] = regions[i].color;
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
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
    }

    private void OnValidate() // called automatically whenever the script values are changes in the inspector
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
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
