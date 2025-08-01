using System.Collections;
using UnityEngine;

namespace Clouds.Simulation
{
	public abstract class ACloudSimulation : MonoBehaviour, ICloudSimulation
	{
		[SerializeField]
		private CloudBox _cloudBox;

		[SerializeField]
		private float _initHumProb = 0.5f;

		[SerializeField]
		private float _initActProb = 0.5f;

		public bool SimulationPlaying {
			get => _simulationPlaying; 
			set {
				if(_simulationPlaying != value) {
					_simulationPlaying = value;
					if(_simulationPlaying) {
						StartCoroutine(TickSimulation_Coroutine());
					}
				}
			}
		}
		public float TickDuration {get; set;} = 1f;

		protected CloudGrid CloudGrid;

		private bool _simulationPlaying;

		private void Start() {
			ResetSimulation();
		}

		public void ResetSimulation() {
			SimulationPlaying = false;
			CloudGrid = _cloudBox.RecreateGrid();
			CloudGrid.InitCells(CreateCell);
		}

		private IEnumerator TickSimulation_Coroutine() {
			while(_simulationPlaying) {
				TickSimulation();
				yield return new WaitForSeconds(TickDuration);
			}
		}

		public void TickSimulation() {
			CloudGrid.UpdateCells(UpdateCell);
		}

		private CloudCell CreateCell() {
			bool hum = UnityEngine.Random.Range(0f, 1f) < _initHumProb;
			CloudCell cell = new(
				hum,
				act: hum && UnityEngine.Random.Range(0f, 1f) < _initActProb,
				cld: false,
				ext: false
			);

			return cell;
		}

		public abstract CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z);
	}
}
