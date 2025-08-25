using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class RenderingSettingsUIController : MonoBehaviour
	{
		[SerializeField]
		private Slider _stepsSlider;

		[SerializeField]
		private TextMeshProUGUI _stepsValueLabel;

		[SerializeField]
		private Slider _lightStepsSlider;

		[SerializeField]
		private TextMeshProUGUI _lightStepsValueLabel;

		[SerializeField]
		private Slider _lightAbsorptionThroughCloudSlider;

		[SerializeField]
		private TextMeshProUGUI _lightAbsorptionThroughCloudValueLabel;

		[SerializeField]
		private Slider _lightAbsorptionTowardSunSlider;

		[SerializeField]
		private TextMeshProUGUI _lightAbsorptionTowardSunValueLabel;

        [SerializeField]
		protected CloudsRendererFeature CloudsRendererFeature;

        protected virtual void OnEnable() {
			_stepsSlider.onValueChanged.AddListener(ChangeStepCount);
			_lightStepsSlider.onValueChanged.AddListener(ChangeLightStepCount);
			_lightAbsorptionThroughCloudSlider.onValueChanged.AddListener(ChangeLightAbsorptionThroughCloud);
			_lightAbsorptionTowardSunSlider.onValueChanged.AddListener(ChangeLightAbsorptionTowardSun);
		}

        protected virtual void OnDisable() {
			_stepsSlider.onValueChanged.RemoveListener(ChangeStepCount);
			_lightStepsSlider.onValueChanged.RemoveListener(ChangeLightStepCount);
            _lightAbsorptionThroughCloudSlider.onValueChanged.AddListener(ChangeLightAbsorptionThroughCloud);
			_lightAbsorptionTowardSunSlider.onValueChanged.AddListener(ChangeLightAbsorptionTowardSun);
		}

        protected virtual void Start() {
			ChangeStepCount(_stepsSlider.value);
			ChangeLightStepCount(_lightStepsSlider.value);
            ChangeLightAbsorptionThroughCloud(_lightAbsorptionThroughCloudSlider.value);
            ChangeLightAbsorptionTowardSun(_lightAbsorptionTowardSunSlider.value);
		}

        private void ChangeStepCount(float value) {
            CloudsRendererFeature.Material.SetInteger("_NumSteps", (int) value);
			_stepsValueLabel.text = value.ToString();
        }

		private void ChangeLightStepCount(float value) {
            CloudsRendererFeature.Material.SetInteger("_NumLightSteps", (int) value);
			_lightStepsValueLabel.text = value.ToString();
        }

		private void ChangeLightAbsorptionThroughCloud(float value) {
            CloudsRendererFeature.Material.SetFloat("_LightAbsorptionThroughCloud", value);
			_lightAbsorptionThroughCloudValueLabel.text = value.ToString("0.00");
        }

		private void ChangeLightAbsorptionTowardSun(float value) {
            CloudsRendererFeature.Material.SetFloat("_LightAbsorptionTowardSun", value);
			_lightAbsorptionTowardSunValueLabel.text = value.ToString("0.00");
        }
    }
}