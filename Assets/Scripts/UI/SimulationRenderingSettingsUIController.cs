using System;
using Clouds.Rendering;
using Clouds.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class SimulationRenderingSettingsUIController : RenderingSettingsUIController
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

		[SerializeField]
		private TMP_Dropdown _renderTypeDropdown;

		[Header("Simulation References")]
		[SerializeField]
		private RaymarchingCloudRenderer _raymarchingCloudRenderer;
		
		[SerializeField]
		private SimpleCloudRenderer _simpleCloudRenderer;

		[SerializeField]
		private CloudBox _cloudBox;

		protected override void OnEnable() {
			base.OnEnable();
			_raymarchingButton.onClick.AddListener(SwitchToRaymarchingMode);
			_cubesButton.onClick.AddListener(SwitchToCubesMode);
			_renderTypeDropdown.onValueChanged.AddListener(SwitchRenderType);
		}

        protected override void OnDisable() {
			base.OnDisable();
			_raymarchingButton.onClick.RemoveListener(SwitchToRaymarchingMode);
			_cubesButton.onClick.RemoveListener(SwitchToCubesMode);
			_renderTypeDropdown.onValueChanged.RemoveListener(SwitchRenderType);
		}

		protected override void Start() {
			base.Start();
			SwitchToRaymarchingMode();
		}

		private void SwitchToRaymarchingMode() {
			_cloudBox.Renderer = _raymarchingCloudRenderer;
			CloudsRendererFeature.SetActive(true);

			_raymarchingButton.interactable = false;
			_cubesButton.interactable = true;

			_raymarchingUI.SetActive(true);
			_cubesUI.SetActive(false);
			_raymarchingCloudRenderer.UpdateDimensions(_cloudBox);
		}

		private void SwitchToCubesMode() {
			_cloudBox.Renderer = _simpleCloudRenderer;
			CloudsRendererFeature.SetActive(false);

			_cubesButton.interactable = false;
			_raymarchingButton.interactable = true;

			_cubesUI.SetActive(true);
			_raymarchingUI.SetActive(false);
			_simpleCloudRenderer.UpdateDimensions(_cloudBox);
		}

        private void SwitchRenderType(int value) {
            string typeName = _renderTypeDropdown.options[value].text;
			SimpleCloudRenderer.RenderType type = (SimpleCloudRenderer.RenderType) Enum.Parse(
					typeof(SimpleCloudRenderer.RenderType), typeName);
			_simpleCloudRenderer.SelectedRenderType = type;
        }
	}
}
