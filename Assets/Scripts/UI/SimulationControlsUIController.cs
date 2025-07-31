using Clouds.Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI.Simulation
{
	public class SimulationControlsUIController : MonoBehaviour
	{
		[SerializeField]
		private Button _playPauseButton;

		[SerializeField]
		private Button _stepButton;

		[SerializeField]
		private Button _resetButton;

		[SerializeField]
		private CloudSimulation _cloudSimulation;


		private void OnEnable() {
			_playPauseButton.onClick.AddListener(PlayPause);
			_stepButton.onClick.AddListener(StepSimulation);
			_resetButton.onClick.AddListener(ResetSimulation);
		}

		private void OnDisable() {
			_playPauseButton.onClick.RemoveListener(PlayPause);
			_resetButton.onClick.RemoveListener(ResetSimulation);
		}

		private void PlayPause() {
			_cloudSimulation.SimulationPlaying = !_cloudSimulation.SimulationPlaying;
			_stepButton.interactable = !_cloudSimulation.SimulationPlaying;
		}

		private void StepSimulation() {
			_cloudSimulation.TickSimulation();
		}

		private void ResetSimulation() {
			_cloudSimulation.ResetSimulation();
			_stepButton.interactable = true;
		}
	}
}
