using UnityEngine;

namespace Clouds.Simulation
{
	public class DefaultCloudSimulation : ACloudSimulation
	{
		[SerializeField]
		private float _extProb = 0.01f;

		public override CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z) {
			return new(
				hum: oldCell.Hum && !oldCell.Act,
				act: !oldCell.Act && oldCell.Hum && ActFunction(x, y, z),
				cld: !oldCell.Ext && oldCell.Cld || oldCell.Act,
				ext: !oldCell.Ext && oldCell.Cld && ExtFunction(x, y, z) 
						|| Random.Range(0f, 1f) < _extProb
			);
        }

		private bool ActFunction(int x, int y, int z) {
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

		private bool ActFunctionWind2(int x, int y, int z) {
			return CloudGrid.GetActInBounds(x + 1, y, z) || CloudGrid.GetActInBounds(x - 1, y, z) 
					|| CloudGrid.GetActInBounds(x, y - 1, z)
					|| CloudGrid.GetActInBounds(x, y, z - 1);
		}

		private bool ExtFunction(int x, int y, int z) {
			return CloudGrid.GetExtInBounds(x + 1, y, z) || CloudGrid.GetExtInBounds(x - 1, y, z) 
					|| CloudGrid.GetExtInBounds(x, y + 1, z) || CloudGrid.GetExtInBounds(x, y - 1, z)
					|| CloudGrid.GetExtInBounds(x, y, z + 1) || CloudGrid.GetExtInBounds(x, y, z - 1) 
					|| CloudGrid.GetExtInBounds(x + 2, y, z) || CloudGrid.GetExtInBounds(x - 2, y, z)
					|| CloudGrid.GetExtInBounds(x, y + 2, z) || CloudGrid.GetExtInBounds(x, y - 2, z)
					|| CloudGrid.GetExtInBounds(x, y, z - 2);
		}
	}
}
