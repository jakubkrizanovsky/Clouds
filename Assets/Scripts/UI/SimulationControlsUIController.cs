using System;
using Clouds.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class SimulationControlsUIController : MonoBehaviour
	{
		[Header("UI References")]
		[SerializeField]
		private Button _playPauseButton;

		[SerializeField]
		private Image _playIcon;

		[SerializeField]
		private Image _pauseIcon;

		[SerializeField]
		private Button _stepButton;

		[SerializeField]
		private Button _resetButton;

		[SerializeField]
		private Slider _simulationSpeedSlider;

		[SerializeField]
		private TextMeshProUGUI _simulationSpeedLabel;


		[Header("Simulation References")]
		[SerializeField]
		private CloudSimulationSelector _cloudSimulation;


		private void OnEnable() {
			_playPauseButton.onClick.AddListener(PlayPause);
			_stepButton.onClick.AddListener(StepSimulation);
			_resetButton.onClick.AddListener(ResetSimulation);
			_simulationSpeedSlider.onValueChanged.AddListener(ChangeSimulationSpeed);
		}

        private void OnDisable() {
			_playPauseButton.onClick.RemoveListener(PlayPause);
			_stepButton.onClick.RemoveListener(StepSimulation);
			_resetButton.onClick.RemoveListener(ResetSimulation);
			_simulationSpeedSlider.onValueChanged.RemoveListener(ChangeSimulationSpeed);
		}

		private void Awake() {
			ChangeSimulationSpeed(_simulationSpeedSlider.value);
		}

		private void PlayPause() {
			_cloudSimulation.SimulationPlaying = !_cloudSimulation.SimulationPlaying;
			_stepButton.interactable = !_cloudSimulation.SimulationPlaying;
			RefreshPlayPauseIcon();
		}

		private void StepSimulation() {
			_cloudSimulation.TickSimulation();
		}

		private void ResetSimulation() {
			_cloudSimulation.ResetSimulation();
			_stepButton.interactable = true;
			RefreshPlayPauseIcon();
		}

		private void RefreshPlayPauseIcon() {
			_playIcon.gameObject.SetActive(!_cloudSimulation.SimulationPlaying);
			_pauseIcon.gameObject.SetActive(_cloudSimulation.SimulationPlaying);
		}

		private void ChangeSimulationSpeed(float value) {
			_cloudSimulation.TickDuration = 1f / value;
            _simulationSpeedLabel.text = value.ToString();
        }
	}
}
