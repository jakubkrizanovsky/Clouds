using Clouds.Rendering;
using Clouds.Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaVR.UI
{
	public class RenderingSettingsUIController : MonoBehaviour
	{
		[SerializeField]
		private Button _raymarchingButton;

		[SerializeField]
		private Button _cubesButton;

		[SerializeField]
		private GameObject _raymarchingUI;

		[SerializeField]
		private GameObject _cubesUI;

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
		}

		private void OnDisable() {
			_raymarchingButton.onClick.RemoveListener(SwitchToRaymarchingMode);
			_cubesButton.onClick.RemoveListener(SwitchToCubesMode);
		}

		private void Start() {
			SwitchToRaymarchingMode();
		}

		private void SwitchToRaymarchingMode() {
			_cloudBox.Renderer = _raymarchingCloudRenderer;
			_cloudsRendererFeature.SetActive(true);

			_raymarchingButton.interactable = false;
			_cubesButton.interactable = true;

			_raymarchingUI.SetActive(true);
			_cubesUI.SetActive(false);
		}

		private void SwitchToCubesMode() {
			_cloudBox.Renderer = _simpleCloudRenderer;
			_cloudsRendererFeature.SetActive(false);

			_cubesButton.interactable = false;
			_raymarchingButton.interactable = true;

			_cubesUI.SetActive(true);
			_raymarchingUI.SetActive(false);
		}
	}
}
