using UnityEngine;
using UnityEngine.UI;

namespace ArenaVR
{
	public class ModeSelectUIController : MonoBehaviour
	{
		[SerializeField]
		private Button _simulationButton;

		[SerializeField]
		private Button _worleyNoiseButton;

		[SerializeField]
		private GameObject _simulationUI;

		[SerializeField]
		private GameObject _worleyNoiseUI;

		[SerializeField]
		private CloudsRendererFeature _simulationRendererFeature;
		
		[SerializeField]
		private CloudsRendererFeature _worleyNoiseRendererFeature;

		private void OnEnable() {
			_simulationButton.onClick.AddListener(SwitchToSimulationMode);
			_worleyNoiseButton.onClick.AddListener(SwitchToWorleyNoiseMode);
		}

		private void OnDisable() {
			_simulationButton.onClick.RemoveListener(SwitchToSimulationMode);
			_worleyNoiseButton.onClick.RemoveListener(SwitchToWorleyNoiseMode);
		}

		private void Start() {
			SwitchToSimulationMode();
		}

		private void SwitchToSimulationMode() {
			_simulationRendererFeature.SetActive(true);
			_worleyNoiseRendererFeature.SetActive(false);

			_simulationButton.interactable = false;
			_worleyNoiseButton.interactable = true;

			_simulationUI.SetActive(true);
			_worleyNoiseUI.SetActive(false);
		}

		private void SwitchToWorleyNoiseMode() {
			_worleyNoiseRendererFeature.SetActive(true);
			_simulationRendererFeature.SetActive(false);

			_worleyNoiseButton.interactable = false;
			_simulationButton.interactable = true;

			_worleyNoiseUI.SetActive(true);
			_simulationUI.SetActive(false);
		}
	}
}
