using UnityEngine;

namespace Clouds.Simulation
{
	public class CloudGrid
	{
    	public readonly Vector3Int Dimensions;
		private readonly float _initHumProb;
		private readonly float _initActProb;
		private readonly float _extProb;

		private CloudCell[,,] _cells;
		private CloudCell[,,] _nextCells;

        public CloudGrid(Vector3Int dimensions, float initHumProb, float initActProb, float extProb) {
            Dimensions = dimensions;
			_initHumProb = initHumProb;
			_initActProb = initActProb;
			_extProb = extProb;
			
			_cells = new CloudCell[dimensions.x, dimensions.y, dimensions.z];
			_nextCells = new CloudCell[dimensions.x, dimensions.y, dimensions.z];

			for(int x = 0; x < Dimensions.x; x++) {
				for(int y = 0; y < Dimensions.y; y++) {
					for(int z = 0; z < Dimensions.z; z++) {
						_cells[x, y, z] = CreateCell();
					}
				}
			}
        }

		private CloudCell CreateCell() {
			bool hum = Random.Range(0f, 1f) < _initHumProb;
			CloudCell cell = new(
				hum,
				act: hum && Random.Range(0f, 1f) < _initActProb,
				cld: false,
				ext: false
			);

			return cell;
		}

		public CloudCell GetCell(int x, int y, int z)
			=> _cells[x, y, z];

		public void Update() {
			for(int x = 0; x < Dimensions.x; x++) {
				for(int y = 0; y < Dimensions.y; y++) {
					for(int z = 0; z < Dimensions.z; z++) {
						_nextCells[x, y, z] = UpdateCell(x, y, z);
					}
				}
			}

            (_nextCells, _cells) = (_cells, _nextCells);
        }

        private CloudCell UpdateCell(int x, int y, int z) {
            CloudCell oldCell = _cells[x, y, z];
			return new(
				hum: oldCell.Hum && !oldCell.Act,
				act: !oldCell.Act && oldCell.Hum && ActFunction(x, y, z),
				cld: !oldCell.Ext && oldCell.Cld || oldCell.Act,
				ext: !oldCell.Ext && oldCell.Cld && ExtFunction(x, y, z) 
						|| Random.Range(0f, 1f) < _extProb
			);
        }

		private bool ActFunction(int x, int y, int z) {
			return GetActInBounds(x + 1, y, z) || GetActInBounds(x - 1, y, z) 
					|| GetActInBounds(x, y + 1, z) || GetActInBounds(x, y - 1, z)
					|| GetActInBounds(x, y, z + 1) || GetActInBounds(x, y, z - 1) 
					|| GetActInBounds(x + 2, y, z) || GetActInBounds(x - 2, y, z)
					|| GetActInBounds(x, y + 2, z) || GetActInBounds(x, y - 2, z)
					|| GetActInBounds(x, y, z - 2);
		}

		private bool ActFunctionWind(int x, int y, int z) {
			return GetActInBounds(x + 1, y, z) || GetActInBounds(x - 1, y, z) 
					|| GetActInBounds(x, y + 1, z) || GetActInBounds(x, y - 1, z)
					|| GetActInBounds(x, y, z - 1);
		}

		private bool ActFunctionWind2(int x, int y, int z) {
			return GetActInBounds(x + 1, y, z) || GetActInBounds(x - 1, y, z) 
					|| GetActInBounds(x, y - 1, z)
					|| GetActInBounds(x, y, z - 1);
		}

		private bool GetActInBounds(int x, int y, int z) {
			if(x < 0 || x >= Dimensions.x || y < 0 || y >= Dimensions.y || z < 0 || z >= Dimensions.z)
				return false;

			return _cells[x, y, z].Act;
		}

		private bool ExtFunction(int x, int y, int z) {
			return GetExtInBounds(x + 1, y, z) || GetExtInBounds(x - 1, y, z) 
					|| GetExtInBounds(x, y + 1, z) || GetExtInBounds(x, y - 1, z)
					|| GetExtInBounds(x, y, z + 1) || GetExtInBounds(x, y, z - 1) 
					|| GetExtInBounds(x + 2, y, z) || GetExtInBounds(x - 2, y, z)
					|| GetExtInBounds(x, y + 2, z) || GetExtInBounds(x, y - 2, z)
					|| GetExtInBounds(x, y, z - 2);
		}

		private bool GetExtInBounds(int x, int y, int z) {
			if(x < 0 || x >= Dimensions.x || y < 0 || y >= Dimensions.y || z < 0 || z >= Dimensions.z)
				return false;

			return _cells[x, y, z].Ext;
		}
    }
}
