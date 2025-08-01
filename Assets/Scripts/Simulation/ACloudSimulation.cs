using System.Collections;
using UnityEngine;

namespace Clouds.Simulation
{
	public abstract class ACloudSimulation : MonoBehaviour, ICloudSimulation
	{
		[SerializeField]
		private CloudBox _cloudBox;

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

		public abstract CloudCell CreateCell();

		public abstract CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z);
	}
}
