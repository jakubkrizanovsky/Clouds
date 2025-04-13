using UnityEngine;

namespace Clouds.Simulation
{
	public class CloudGrid
	{
    	public readonly Vector3Int Dimensions;
		private CloudCell[,,] _cells;

        public CloudGrid(Vector3Int dimensions) {
            Dimensions = dimensions;
			_cells = new CloudCell[dimensions.x, dimensions.y, dimensions.z];

			for(int x = 0; x < Dimensions.x; x++) {
				for(int y = 0; y < Dimensions.y; y++) {
					for(int z = 0; z < Dimensions.z; z++) {
						_cells[x, y, z] = CreateCell(x, y, z);
					}
				}
			}
        }

		private CloudCell CreateCell(int x, int y, int z) {
			CloudCell cell = new(x, y, z);
			cell.cld = Random.Range(0f, 1f) > 0.5f;
			return cell;
		}

		public CloudCell GetCell(int x, int y, int z)
			=> _cells[x, y, z];
    }
}
