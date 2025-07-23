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
			_simulationButton.interactable = false;
			_worleyNoiseButton.interactable = true;

			_simulationUI.SetActive(true);
			_worleyNoiseUI.SetActive(false);
		}

		private void SwitchToWorleyNoiseMode() {
			_worleyNoiseButton.interactable = false;
			_simulationButton.interactable = true;

			_worleyNoiseUI.SetActive(true);
			_simulationUI.SetActive(false);
		}
	}
}
