using Clouds.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clouds.UI
{
	public class SizeSettingsUIController : MonoBehaviour
	{
		[SerializeField]
		private Slider _sizeXZSlider;

		[SerializeField]
		private TextMeshProUGUI _sizeXZValueLabel;

		[SerializeField]
		private Slider _sizeYSlider;

		[SerializeField]
		private TextMeshProUGUI _sizeYValueLabel;

		[SerializeField]
		private Slider _gridScaleSlider;

		[SerializeField]
		private TextMeshProUGUI _gridScaleValueLabel;

		[field: SerializeField]
		protected CloudBox CloudBox {get; private set;}

		private void OnEnable() {
			_sizeXZSlider.onValueChanged.AddListener(ChangeSizeXZ);
			_sizeYSlider.onValueChanged.AddListener(ChangeSizeY);
			if(_gridScaleSlider != null) {
				_gridScaleSlider.onValueChanged.AddListener(ChangeGridScale);
			}
		}

        private void OnDisable() {
			_sizeXZSlider.onValueChanged.RemoveListener(ChangeSizeXZ);
			_sizeYSlider.onValueChanged.RemoveListener(ChangeSizeY);
			if(_gridScaleSlider != null) {
				_gridScaleSlider.onValueChanged.RemoveListener(ChangeGridScale);
			}
		}

		private void Start() {
			ChangeSizeXZ(_sizeXZSlider.value);
			ChangeSizeY(_sizeYSlider.value);
			if(_gridScaleSlider != null) {
				ChangeGridScale(_gridScaleSlider.value);
			}
		}

		protected virtual void ChangeSizeXZ(float value) {
			CloudBox.transform.localScale = new(value, CloudBox.transform.localScale.y, value);
			_sizeXZValueLabel.text = value.ToString("0");
        }

		protected virtual void ChangeSizeY(float value) {
			CloudBox.transform.localScale = new(CloudBox.transform.localScale.x, value, CloudBox.transform.localScale.z);
			_sizeYValueLabel.text = value.ToString("0");
        }

		private void ChangeGridScale(float value) {
			CloudBox.GridScale = value;
			_gridScaleValueLabel.text = value.ToString("0");
        }
	}
}
