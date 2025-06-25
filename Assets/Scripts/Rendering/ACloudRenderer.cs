using Clouds.Simulation;
using UnityEngine;

namespace Clouds.Rendering
{
	public abstract class ACloudRenderer : MonoBehaviour
	{
    	public abstract void Render(CloudGrid cloudGrid, float gridScale);

		public virtual void UpdateDimensions(CloudBox cloudBox) {}
	}
}
