using System;
using Clouds.Rendering;
using Clouds.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class RenderingSettingsUIController : MonoBehaviour
	{
		[Header("UI References")]
		[SerializeField]
		private Button _raymarchingButton;

		[SerializeField]
		private Button _cubesButton;

		[SerializeField]
		private GameObject _raymarchingUI;

		[SerializeField]
		private GameObject _cubesUI;

		[Header("Raymarching UI")]
		[SerializeField]
		private Slider _stepsSlider;

		[SerializeField]
		private TextMeshProUGUI _stepsValueLabel;

		[SerializeField]
		private Slider _lightStepsSlider;

		[SerializeField]
		private TextMeshProUGUI _lightStepsValueLabel;

		[Header("Cubes UI")]
		[SerializeField]
		private TMP_Dropdown _renderTypeDropdown;

		[Header("Simulation References")]
		[SerializeField]
		private RaymarchingCloudRenderer _raymarchingCloudRenderer;
		
		[SerializeField]
		private SimpleCloudRenderer _simpleCloudRenderer;

		[SerializeField]
		private CloudBox _cloudBox;

		[SerializeField]
		private CloudsRendererFeature _cloudsRendererFeature;

		private void OnEnable() {
			_raymarchingButton.onClick.AddListener(SwitchToRaymarchingMode);
			_cubesButton.onClick.AddListener(SwitchToCubesMode);
			_renderTypeDropdown.onValueChanged.AddListener(SwitchRenderType);
			_stepsSlider.onValueChanged.AddListener(ChangeStepCount);
			_lightStepsSlider.onValueChanged.AddListener(ChangeLightStepCount);
		}

        private void OnDisable() {
			_raymarchingButton.onClick.RemoveListener(SwitchToRaymarchingMode);
			_cubesButton.onClick.RemoveListener(SwitchToCubesMode);
			_renderTypeDropdown.onValueChanged.RemoveListener(SwitchRenderType);
			_stepsSlider.onValueChanged.RemoveListener(ChangeStepCount);
			_lightStepsSlider.onValueChanged.RemoveListener(ChangeLightStepCount);
		}

		private void Start() {
			SwitchToRaymarchingMode();
			ChangeStepCount(_stepsSlider.value);
			ChangeLightStepCount(_lightStepsSlider.value);
		}

		private void SwitchToRaymarchingMode() {
			_cloudBox.Renderer = _raymarchingCloudRenderer;
			_cloudsRendererFeature.SetActive(true);

			_raymarchingButton.interactable = false;
			_cubesButton.interactable = true;

			_raymarchingUI.SetActive(true);
			_cubesUI.SetActive(false);
			_raymarchingCloudRenderer.UpdateDimensions(_cloudBox);
		}

		private void SwitchToCubesMode() {
			_cloudBox.Renderer = _simpleCloudRenderer;
			_cloudsRendererFeature.SetActive(false);

			_cubesButton.interactable = false;
			_raymarchingButton.interactable = true;

			_cubesUI.SetActive(true);
			_raymarchingUI.SetActive(false);
			_simpleCloudRenderer.UpdateDimensions(_cloudBox);
		}

		private void ChangeStepCount(float value) {
            _cloudsRendererFeature.Material.SetInteger("_NumSteps", (int) value);
			_stepsValueLabel.text = value.ToString();
        }

		private void ChangeLightStepCount(float value) {
            _cloudsRendererFeature.Material.SetInteger("_NumLightSteps", (int) value);
			_lightStepsValueLabel.text = value.ToString();
        }

        private void SwitchRenderType(int value) {
            string typeName = _renderTypeDropdown.options[value].text;
			SimpleCloudRenderer.RenderType type = (SimpleCloudRenderer.RenderType) Enum.Parse(
					typeof(SimpleCloudRenderer.RenderType), typeName);
			_simpleCloudRenderer.SelectedRenderType = type;
        }
	}
}
