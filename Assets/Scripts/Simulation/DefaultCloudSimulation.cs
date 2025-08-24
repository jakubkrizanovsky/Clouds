using UnityEngine;

namespace Clouds.Simulation
{
	public class DefaultCloudSimulation : ACloudSimulation
	{
		[field: SerializeField]
		public float InitHumProb {get; set;} = 0.3f;

		[field: SerializeField]
		public float InitActProb {get; set;} = 0.5f;

		[field: SerializeField]
		public float ExtProb {get; set;} = 0.01f;

		public override CloudCell CreateCell() {
			bool hum = UnityEngine.Random.Range(0f, 1f) < InitHumProb;
			CloudCell cell = new(
				hum,
				act: hum && UnityEngine.Random.Range(0f, 1f) < InitActProb,
				cld: false,
				ext: false
			);

			return cell;
		}

		public override CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z) {
			return new(
				hum: oldCell.Hum && !oldCell.Act,
				act: !oldCell.Act && oldCell.Hum && ActFunction(x, y, z),
				cld: !oldCell.Ext && oldCell.Cld || oldCell.Act,
				ext: !oldCell.Ext && oldCell.Cld && ExtFunction(x, y, z) 
						|| Random.Range(0f, 1f) < ExtProb
			);
        }
	}
}
