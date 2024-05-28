using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {   
                // since the noiseMap is a 2D array we're mapping the indices of that into a 1D array in the colorMap
                // the formula for that is array[row * width + col]. i.e, recaclulation of 2D array indices into 1D array index.
                colorMap[z * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, z]); 
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture; //sharedMaterial will help apply the texture in the editor rather 
        // than instantiating on runtime 
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }
}
