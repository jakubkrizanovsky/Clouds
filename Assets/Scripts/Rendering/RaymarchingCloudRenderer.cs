using Clouds.Simulation;
using UnityEngine;

namespace Clouds.Rendering
{
    public class RaymarchingCloudRenderer : ACloudRenderer
    {
        [SerializeField]
        private CloudsRendererFeature _cloudsRendererFeature;
        
        public override void Render(CloudGrid cloudGrid, float gridScale) {
            //TODO
        }

        public override void UpdateDimensions(CloudBox cloudBox) {
            Vector3 boundsMin = cloudBox.transform.position - 0.5f * cloudBox.transform.localScale;
            Vector3 boundsMax = cloudBox.transform.position + 0.5f * cloudBox.transform.localScale;
            
            _cloudsRendererFeature.MaterialInstance.SetVector("_BoundsMin", boundsMin);
            _cloudsRendererFeature.MaterialInstance.SetVector("_BoundsMax", boundsMax);
        }
    }
}