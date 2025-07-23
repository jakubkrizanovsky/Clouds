using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Clouds.TestStuff
{
	public class TestTextureGenerator : MonoBehaviour
	{
#if UNITY_EDITOR
		[MenuItem("CreateExamples/3DTexture")]
#endif
		static void CreateTestTexture() {
			// Set the texture parameters
			int sizeX = 5;
			int sizeY = 5;
			int sizeZ = 5;
			float probability = 0.3f;
			TextureFormat format = TextureFormat.R8;
			TextureWrapMode wrapMode =  TextureWrapMode.Clamp;

			// Create the texture and apply the parameters
			Texture3D texture = new Texture3D(sizeX, sizeY, sizeZ, format, false);
			texture.wrapMode = wrapMode;

			// Create a 3-dimensional array to store color data
			Color[] colors = new Color[sizeX * sizeY * sizeZ];

			for (int z = 0; z < sizeZ; z++)
			{
				int zOffset = z * sizeX * sizeY;
				for (int y = 0; y < sizeY; y++)
				{
					int yOffset = y * sizeX;
					for (int x = 0; x < sizeX; x++)
					{
						bool set = Random.Range(0f, 1f) < probability;
						colors[x + yOffset + zOffset] = set ? Color.white : Color.black;
					}
				}
			}

			// Copy the color values to the texture
			texture.SetPixels(colors);

			// Apply the changes to the texture and upload the updated texture to the GPU
			texture.Apply();        

			// Save the texture to your Unity Project
			AssetDatabase.CreateAsset(texture, "Assets/Textures/CreatedTestTexture.asset");
		
		}
	}
}
