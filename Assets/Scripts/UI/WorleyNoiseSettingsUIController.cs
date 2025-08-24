using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Clouds.UI
{
	public class WorleyNoiseSettingsUIController : MonoBehaviour
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
		private CloudsRendererFeature _worleyNoiseRendererFeature;

		private void OnEnable() {
			_stepsSlider.onValueChanged.AddListener(ChangeStepCount);
			_lightStepsSlider.onValueChanged.AddListener(ChangeLightStepCount);
		}

        private void OnDisable() {
			_stepsSlider.onValueChanged.RemoveListener(ChangeStepCount);
			_lightStepsSlider.onValueChanged.RemoveListener(ChangeLightStepCount);
		}

		private void Start() {
			ChangeStepCount(_stepsSlider.value);
			ChangeLightStepCount(_lightStepsSlider.value);
		}

		private void ChangeStepCount(float value) {
            _worleyNoiseRendererFeature.Material.SetInteger("_NumSteps", (int) value);
			_stepsValueLabel.text = value.ToString();
        }

		private void ChangeLightStepCount(float value) {
            _worleyNoiseRendererFeature.Material.SetInteger("_NumLightSteps", (int) value);
			_lightStepsValueLabel.text = value.ToString();
        }
	}
}
