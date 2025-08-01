using Clouds.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class CustomSimulationSettingsUIController : MonoBehaviour
	{
		[SerializeField]
		private Slider _humSlider;

		[SerializeField]
		private TextMeshProUGUI _humValueLabel;

		[SerializeField]
		private Slider _actSlider;

		[SerializeField]
		private TextMeshProUGUI _actValueLabel;

		[SerializeField]
		private Slider _extSlider;

		[SerializeField]
		private TextMeshProUGUI _extValueLabel;

		[SerializeField]
		private CustomCloudSimulation _customCloudSimulation;

		private void OnEnable() {
			_humSlider.onValueChanged.AddListener(ChangeHumProb);
			_actSlider.onValueChanged.AddListener(ChangeActProb);
			_extSlider.onValueChanged.AddListener(ChangeExtProb);
		}

        private void OnDisable() {
			_humSlider.onValueChanged.RemoveListener(ChangeHumProb);
			_actSlider.onValueChanged.RemoveListener(ChangeActProb);
			_extSlider.onValueChanged.RemoveListener(ChangeExtProb);
		}

		private void Start() {
			ChangeHumProb(_humSlider.value);
			ChangeActProb(_actSlider.value);
			ChangeExtProb(_extSlider.value);
		}

		private void ChangeHumProb(float value) {
			_customCloudSimulation.HumProb = value;
			_humValueLabel.text = value.ToString("0.00");
        }

		private void ChangeActProb(float value) {
			_customCloudSimulation.ActProb = value;
			_actValueLabel.text = value.ToString("0.00");
        }

		private void ChangeExtProb(float value) {
			_customCloudSimulation.ExtProb = value;
			_extValueLabel.text = value.ToString("0.00");
        }
	}
}
