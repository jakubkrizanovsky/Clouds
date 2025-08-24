using Clouds.Simulation;
using UnityEngine;

namespace Clouds.UI
{
	public class WorleyNoiseSizeSettingsUIControlller : SizeSettingsUIController
	{
		[SerializeField]
		private CloudsRendererFeature _worleyNoiseRendererFeature;

        protected override void ChangeSizeXZ(float value) {
            base.ChangeSizeXZ(value);
			UpdateDimensions();
        }

        protected override void ChangeSizeY(float value) {
            base.ChangeSizeY(value);
			UpdateDimensions();
        }
		
		private void UpdateDimensions() {
            Vector3 boundsMin = CloudBox.transform.position - 0.5f * CloudBox.transform.localScale;
            Vector3 boundsMax = CloudBox.transform.position + 0.5f * CloudBox.transform.localScale;
            
            _worleyNoiseRendererFeature.Material.SetVector("_BoundsMin", boundsMin);
            _worleyNoiseRendererFeature.Material.SetVector("_BoundsMax", boundsMax);
        }
	}
}
