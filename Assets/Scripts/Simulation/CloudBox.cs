using System.Collections;
using Clouds.Rendering;
using UnityEngine;

namespace Clouds.Simulation
{
	public class CloudBox : MonoBehaviour
	{
    	[SerializeField]
		private float _gridScale;

		[field: SerializeField]
		public ACloudRenderer Renderer {get; set;}

		[SerializeField]
		private bool _drawGrid = false;

		private CloudGrid _grid;

		public CloudGrid RecreateGrid() {
			_grid = new CloudGrid(CalculateGridDimesions());
			return _grid;
		}

		private void Update() {
			if(Renderer != null) {
				Renderer.Render(_grid, _gridScale);
				
#if UNITY_EDITOR
				Renderer.UpdateDimensions(this);
#endif
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

		private Vector3 CalculateCellCenter(int x, int y, int z, Vector3Int gridDimensions) {
			return transform.position + _gridScale * new Vector3(x, y, z) 
					- 0.5f * _gridScale * (Vector3)gridDimensions + 0.5f * _gridScale * Vector3.one;
		}
	}
}
