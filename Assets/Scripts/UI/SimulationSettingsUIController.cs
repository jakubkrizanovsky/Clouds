using Clouds.Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class SimulationSettingsUIController : MonoBehaviour
	{
		[SerializeField]
		private Button _defaultButton;

		[SerializeField]
		private Button _customButton;

		[SerializeField]
		private GameObject _defaultUI;

		[SerializeField]
		private GameObject _customUI;

		[SerializeField]
		private SimulationControlsUIController _simulationControlsUIController;

		[SerializeField]
		private CloudSimulationSelector _cloudSimulationSelector;

		private void OnEnable() {
			_defaultButton.onClick.AddListener(SwitchToDefaultMode);
			_customButton.onClick.AddListener(SwitchToCustomMode);
		}

		private void OnDisable() {
			_defaultButton.onClick.RemoveListener(SwitchToDefaultMode);
			_customButton.onClick.RemoveListener(SwitchToCustomMode);
		}

		private void Start() {
			SwitchToDefaultMode();
		}

		private void SwitchToDefaultMode() {
			_cloudSimulationSelector.CurrentSimulation = _cloudSimulationSelector.DefaultCloudSimulation;
			_simulationControlsUIController.ResetSimulation();

			_defaultButton.interactable = false;
			_customButton.interactable = true;

			_defaultUI.SetActive(true);
			_customUI.SetActive(false);
		}

		private void SwitchToCustomMode() {
			_cloudSimulationSelector.CurrentSimulation = _cloudSimulationSelector.CustomCloudSimulation;
			_simulationControlsUIController.ResetSimulation();

			_customButton.interactable = false;
			_defaultButton.interactable = true;

			_customUI.SetActive(true);
			_defaultUI.SetActive(false);
		}
	}
}
