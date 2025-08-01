using System;
using UnityEngine;

namespace Clouds.Simulation
{
	public class CloudGrid
	{
    	public readonly Vector3Int Dimensions;

		private CloudCell[,,] _cells;
		private CloudCell[,,] _nextCells;

        public CloudGrid(Vector3Int dimensions) {
            Dimensions = dimensions;
			
			_cells = new CloudCell[dimensions.x, dimensions.y, dimensions.z];
			_nextCells = new CloudCell[dimensions.x, dimensions.y, dimensions.z];
        }

		public void InitCells(Func<CloudCell> createCellFunction) {
			for(int x = 0; x < Dimensions.x; x++) {
				for(int y = 0; y < Dimensions.y; y++) {
					for(int z = 0; z < Dimensions.z; z++) {
						_cells[x, y, z] = createCellFunction.Invoke();
					}
				}
			}
		}

		public void UpdateCells(Func<CloudCell, int, int, int, CloudCell> updateCellFunction) {
			for(int x = 0; x < Dimensions.x; x++) {
				for(int y = 0; y < Dimensions.y; y++) {
					for(int z = 0; z < Dimensions.z; z++) {
						_nextCells[x, y, z] = updateCellFunction.Invoke(_cells[x, y, z], x, y, z);
					}
				}
			}

            (_nextCells, _cells) = (_cells, _nextCells);
        }

		public CloudCell GetCell(int x, int y, int z)
			=> _cells[x, y, z];

		public bool GetActInBounds(int x, int y, int z) {
			if(x < 0 || x >= Dimensions.x || y < 0 || y >= Dimensions.y || z < 0 || z >= Dimensions.z)
				return false;

			return _cells[x, y, z].Act;
		}

		public bool GetExtInBounds(int x, int y, int z) {
			if(x < 0 || x >= Dimensions.x || y < 0 || y >= Dimensions.y || z < 0 || z >= Dimensions.z)
				return false;

			return _cells[x, y, z].Ext;
		}

		public bool GetHumInBounds(int x, int y, int z) {
			if(x < 0 || x >= Dimensions.x || y < 0 || y >= Dimensions.y || z < 0 || z >= Dimensions.z)
				return false;

			return _cells[x, y, z].Hum;
		}
	}
}
