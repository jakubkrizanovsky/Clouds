using Clouds.Simulation;
using UnityEditor;
using UnityEngine;

namespace Clouds.Rendering
{
    public class RaymarchingCloudRenderer : ACloudRenderer
    {
        [SerializeField]
        private CloudsRendererFeature _cloudsRendererFeature;

        private Texture3D _texture;
        
        public override void Render(CloudGrid cloudGrid, float gridScale) {
            if(_texture == null) {                
                _texture = new Texture3D(cloudGrid.Dimensions.x, cloudGrid.Dimensions.y, 
                        cloudGrid.Dimensions.z, TextureFormat.R8, false);
                _cloudsRendererFeature.MaterialInstance.SetTexture("_NoiseTexture", _texture);
            }
            
            for(int i = 0; i < cloudGrid.Dimensions.x; i++) {
                for(int j = 0; j < cloudGrid.Dimensions.y; j++) {
                    for(int k = 0; k < cloudGrid.Dimensions.z; k++) {
                        _texture.SetPixel(i, j, k, cloudGrid.GetCell(i, j, k).Cld ? Color.white : Color.black);
                    }
                }
            }
            _texture.Apply();
        }

        public override void UpdateDimensions(CloudBox cloudBox) {
            Vector3 boundsMin = cloudBox.transform.position - 0.5f * cloudBox.transform.localScale;
            Vector3 boundsMax = cloudBox.transform.position + 0.5f * cloudBox.transform.localScale;
            
            _cloudsRendererFeature.MaterialInstance.SetVector("_BoundsMin", boundsMin);
            _cloudsRendererFeature.MaterialInstance.SetVector("_BoundsMax", boundsMax);
        }
    }
}