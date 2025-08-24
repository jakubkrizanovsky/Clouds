using System.Collections;
using UnityEngine;

namespace Clouds.Simulation
{
	public abstract class ACloudSimulation : MonoBehaviour, ICloudSimulation
	{
		[field: SerializeField]
		public bool UseWindEffect {get; set;} = false;

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

		protected bool ActFunction(int x, int y, int z) {
			return UseWindEffect ? ActFunctionWind(x, y, z) : ActFunctionDefault(x, y, z);
		}

		private bool ActFunctionDefault(int x, int y, int z) {
			return CloudGrid.GetActInBounds(x + 1, y, z) || CloudGrid.GetActInBounds(x - 1, y, z) 
					|| CloudGrid.GetActInBounds(x, y + 1, z) || CloudGrid.GetActInBounds(x, y - 1, z)
					|| CloudGrid.GetActInBounds(x, y, z + 1) || CloudGrid.GetActInBounds(x, y, z - 1) 
					|| CloudGrid.GetActInBounds(x + 2, y, z) || CloudGrid.GetActInBounds(x - 2, y, z)
					|| CloudGrid.GetActInBounds(x, y + 2, z) || CloudGrid.GetActInBounds(x, y - 2, z)
					|| CloudGrid.GetActInBounds(x, y, z - 2);
		}

		private bool ActFunctionWind(int x, int y, int z) {
			return CloudGrid.GetActInBounds(x + 1, y, z) || CloudGrid.GetActInBounds(x - 1, y, z) 
					|| CloudGrid.GetActInBounds(x, y + 1, z) || CloudGrid.GetActInBounds(x, y - 1, z)
					|| CloudGrid.GetActInBounds(x, y, z - 1);
		}

		protected bool ExtFunction(int x, int y, int z) {
			return CloudGrid.GetExtInBounds(x + 1, y, z) || CloudGrid.GetExtInBounds(x - 1, y, z) 
					|| CloudGrid.GetExtInBounds(x, y + 1, z) || CloudGrid.GetExtInBounds(x, y - 1, z)
					|| CloudGrid.GetExtInBounds(x, y, z + 1) || CloudGrid.GetExtInBounds(x, y, z - 1) 
					|| CloudGrid.GetExtInBounds(x + 2, y, z) || CloudGrid.GetExtInBounds(x - 2, y, z)
					|| CloudGrid.GetExtInBounds(x, y + 2, z) || CloudGrid.GetExtInBounds(x, y - 2, z)
					|| CloudGrid.GetExtInBounds(x, y, z - 2);
		}
	}
}
