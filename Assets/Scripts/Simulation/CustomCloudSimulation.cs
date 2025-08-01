using UnityEngine;

namespace Clouds.Simulation
{
	public class CustomCloudSimulation : ACloudSimulation
	{
		[field: SerializeField]
		public float ActProb {get; set;} = 0.01f;

		[field: SerializeField]
		public float ExtProb {get; set;} = 0.01f;

		[field: SerializeField]
		public float HumProb {get; set;} = 0.3f;

		public override CloudCell CreateCell() {
			CloudCell cell = new(
				hum: false,
				act: false,
				cld: false,
				ext: false
			);

			return cell;
		}

		public override CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z) {
			return new(
				hum: (oldCell.Hum && !oldCell.Act) || HumFunction(x, y, z),
				act: !oldCell.Act && oldCell.Hum && (ActFunction(x, y, z)
						|| Random.Range(0f, 1f) < ActProb),
				cld: !oldCell.Ext && oldCell.Cld || oldCell.Act,
				ext: !oldCell.Ext && oldCell.Cld && (ExtFunction(x, y, z) 
						|| Random.Range(0f, 1f) < ExtProb)
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

		private bool ExtFunction(int x, int y, int z) {
			return CloudGrid.GetExtInBounds(x + 1, y, z) || CloudGrid.GetExtInBounds(x - 1, y, z) 
					|| CloudGrid.GetExtInBounds(x, y + 1, z) || CloudGrid.GetExtInBounds(x, y - 1, z)
					|| CloudGrid.GetExtInBounds(x, y, z + 1) || CloudGrid.GetExtInBounds(x, y, z - 1) 
					|| CloudGrid.GetExtInBounds(x + 2, y, z) || CloudGrid.GetExtInBounds(x - 2, y, z)
					|| CloudGrid.GetExtInBounds(x, y + 2, z) || CloudGrid.GetExtInBounds(x, y - 2, z)
					|| CloudGrid.GetExtInBounds(x, y, z - 2);
		}

		private bool HumFunction(int x, int y, int z) {
			return Random.Range(0f, 1f) < HumProb;
			if(y == 0) {
				return Random.Range(0f, 1f) < HumProb;
			} else {
				return CloudGrid.GetHumInBounds(x, y - 1, z) && Random.Range(0f, 1f) < HumProb;
			}
		}
	}
}
