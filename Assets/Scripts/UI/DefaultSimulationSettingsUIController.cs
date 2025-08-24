using Clouds.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class DefaultSimulationSettingsUIController : MonoBehaviour
	{
		[SerializeField]
		private Slider _initHumSlider;

		[SerializeField]
		private TextMeshProUGUI _initHumValueLabel;

		[SerializeField]
		private Slider _initActSlider;

		[SerializeField]
		private TextMeshProUGUI _initActValueLabel;

		[SerializeField]
		private Slider _extSlider;

		[SerializeField]
		private TextMeshProUGUI _extValueLabel;

		[SerializeField]
		private Toggle _useWindEffectToggle;

		[SerializeField]
		private DefaultCloudSimulation _defaultCloudSimulation;

		private void OnEnable() {
			_initHumSlider.onValueChanged.AddListener(ChangeInitHumProb);
			_initActSlider.onValueChanged.AddListener(ChangeInitActProb);
			_extSlider.onValueChanged.AddListener(ChangeExtProb);
			_useWindEffectToggle.onValueChanged.AddListener(ToggleWindEffect);
		}

        private void OnDisable() {
			_initHumSlider.onValueChanged.RemoveListener(ChangeInitHumProb);
			_initActSlider.onValueChanged.RemoveListener(ChangeInitActProb);
			_extSlider.onValueChanged.RemoveListener(ChangeExtProb);
			_useWindEffectToggle.onValueChanged.RemoveListener(ToggleWindEffect);
		}

		private void Start() {
			ChangeInitHumProb(_initHumSlider.value);
			ChangeInitActProb(_initActSlider.value);
			ChangeExtProb(_extSlider.value);
		}

		private void ChangeInitHumProb(float value) {
			_defaultCloudSimulation.InitHumProb = value;
			_initHumValueLabel.text = value.ToString("0.00");
        }

		private void ChangeInitActProb(float value) {
			_defaultCloudSimulation.InitActProb = value;
			_initActValueLabel.text = value.ToString("0.00");
        }

		private void ChangeExtProb(float value) {
			_defaultCloudSimulation.ExtProb = value;
			_extValueLabel.text = value.ToString("0.00");
        }

		private void ToggleWindEffect(bool value) {
			_defaultCloudSimulation.UseWindEffect = value;
		}
	}
}
