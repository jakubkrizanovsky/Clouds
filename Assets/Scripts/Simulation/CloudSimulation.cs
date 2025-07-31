using System.Collections;
using UnityEngine;

namespace Clouds.Simulation
{
	public class CloudSimulation : MonoBehaviour
	{
		[SerializeField]
		private CloudBox _cloudBox;

		[SerializeField]
		private float _tickDuration = 1f;

		[SerializeField]
		private float _initHumProb = 0.5f;

		[SerializeField]
		private float _initActProb = 0.5f;
		
		[SerializeField]
		private float _extProb = 0.01f;

		private CloudGrid _cloudGrid;

		private bool _simulationPlaying;
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

		private void Start() {
			ResetSimulation();
		}

		public void ResetSimulation() {
			SimulationPlaying = false;
			_cloudGrid = _cloudBox.RecreateGrid();
			_cloudGrid.InitCells(CreateCell);
		}

		private IEnumerator TickSimulation_Coroutine() {
			while(_simulationPlaying) {
				TickSimulation();
				yield return new WaitForSeconds(_tickDuration);
			}
		}

		public void TickSimulation() {
			_cloudGrid.UpdateCells(UpdateCell);
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

		private CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z) {
			return new(
				hum: oldCell.Hum && !oldCell.Act,
				act: !oldCell.Act && oldCell.Hum && ActFunction(x, y, z),
				cld: !oldCell.Ext && oldCell.Cld || oldCell.Act,
				ext: !oldCell.Ext && oldCell.Cld && ExtFunction(x, y, z) 
						|| Random.Range(0f, 1f) < _extProb
			);
        }

		private bool ActFunction(int x, int y, int z) {
			return _cloudGrid.GetActInBounds(x + 1, y, z) || _cloudGrid.GetActInBounds(x - 1, y, z) 
					|| _cloudGrid.GetActInBounds(x, y + 1, z) || _cloudGrid.GetActInBounds(x, y - 1, z)
					|| _cloudGrid.GetActInBounds(x, y, z + 1) || _cloudGrid.GetActInBounds(x, y, z - 1) 
					|| _cloudGrid.GetActInBounds(x + 2, y, z) || _cloudGrid.GetActInBounds(x - 2, y, z)
					|| _cloudGrid.GetActInBounds(x, y + 2, z) || _cloudGrid.GetActInBounds(x, y - 2, z)
					|| _cloudGrid.GetActInBounds(x, y, z - 2);
		}

		private bool ActFunctionWind(int x, int y, int z) {
			return _cloudGrid.GetActInBounds(x + 1, y, z) || _cloudGrid.GetActInBounds(x - 1, y, z) 
					|| _cloudGrid.GetActInBounds(x, y + 1, z) || _cloudGrid.GetActInBounds(x, y - 1, z)
					|| _cloudGrid.GetActInBounds(x, y, z - 1);
		}

		private bool ActFunctionWind2(int x, int y, int z) {
			return _cloudGrid.GetActInBounds(x + 1, y, z) || _cloudGrid.GetActInBounds(x - 1, y, z) 
					|| _cloudGrid.GetActInBounds(x, y - 1, z)
					|| _cloudGrid.GetActInBounds(x, y, z - 1);
		}

		private bool ExtFunction(int x, int y, int z) {
			return _cloudGrid.GetExtInBounds(x + 1, y, z) || _cloudGrid.GetExtInBounds(x - 1, y, z) 
					|| _cloudGrid.GetExtInBounds(x, y + 1, z) || _cloudGrid.GetExtInBounds(x, y - 1, z)
					|| _cloudGrid.GetExtInBounds(x, y, z + 1) || _cloudGrid.GetExtInBounds(x, y, z - 1) 
					|| _cloudGrid.GetExtInBounds(x + 2, y, z) || _cloudGrid.GetExtInBounds(x - 2, y, z)
					|| _cloudGrid.GetExtInBounds(x, y + 2, z) || _cloudGrid.GetExtInBounds(x, y - 2, z)
					|| _cloudGrid.GetExtInBounds(x, y, z - 2);
		}

		
	}
}
