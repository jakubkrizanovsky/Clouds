using System.Collections;
using Clouds.Rendering;
using UnityEngine;

namespace Clouds.Simulation
{
	public class CloudBox : MonoBehaviour
	{
    	[SerializeField]
		private float _gridScale;

		[SerializeField]
		private float _tickDuration = 1f;

		[SerializeField]
		private float _initHumProb = 0.5f;

		[SerializeField]
		private float _initActProb = 0.5f;

		[SerializeField]
		private ACloudRenderer _renderer;

		[SerializeField]
		private bool _drawGrid = false;

		private CloudGrid _grid;

		private void Start() {
			_grid = new CloudGrid(CalculateGridDimesions(), _initHumProb, _initActProb);
			StartCoroutine(TickSimulation_Coroutine());
		}

		private void Update() {
			if(_renderer != null) {
				_renderer.Render(_grid, _gridScale);
			}
		}

		private IEnumerator TickSimulation_Coroutine() {
			while(true) {
				yield return new WaitForSeconds(_tickDuration);
				_grid.Update();
			}
		}

		private Vector3Int CalculateGridDimesions() {
			Vector3 floatDimensions = transform.localScale / _gridScale;
			return new Vector3Int(
				(int) floatDimensions.x, 
				(int) floatDimensions.y,
				(int) floatDimensions.z
			);
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, transform.localScale);

			// if(_grid != null)
			// 	Visualize();

			if(!_drawGrid) return;

			Gizmos.color = Color.yellow;
			Vector3Int dimensions = CalculateGridDimesions();
			for(int x = 0; x < dimensions.x; x++) {
				for(int y = 0; y < dimensions.y; y++) {
					for(int z = 0; z < dimensions.z; z++) {
						Vector3 cellPosition = CalculateCellCenter(x, y, z, dimensions);
						Gizmos.DrawWireCube(cellPosition, _gridScale * Vector3.one);
					}
				}
			}

			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, transform.localScale);
		}

		// private void Visualize() {
		// 	Gizmos.color = Color.white;
		// 	for(int x = 0; x < _grid.Dimensions.x; x++) {
		// 		for(int y = 0; y < _grid.Dimensions.y; y++) {
		// 			for(int z = 0; z < _grid.Dimensions.z; z++) {
		// 				CloudCell cell = _grid.GetCell(x, y, z);
		// 				if(cell.Cld) {
		// 					Vector3 cellPosition = CalculateCellCenter(x, y, z, _grid.Dimensions);
		// 					Gizmos.DrawCube(cellPosition, _gridScale * Vector3.one);
		// 				}
		// 			}
		// 		}
		// 	}
		// }

		private Vector3 CalculateCellCenter(int x, int y, int z, Vector3Int gridDimensions) {
			return transform.position + _gridScale * new Vector3(x, y, z) 
					- 0.5f * _gridScale * (Vector3)gridDimensions + 0.5f * _gridScale * Vector3.one;
		}
	}
}
