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

		private bool HumFunction(int x, int y, int z) {
			return Random.Range(0f, 1f) < HumProb;
			if(x == 0) {
				return Random.Range(0f, 1f) < HumProb;
			} else {
				return CloudGrid.GetHumInBounds(x - 1, y, z) && Random.Range(0f, 1f) < HumProb;
			}
		}
	}
}
