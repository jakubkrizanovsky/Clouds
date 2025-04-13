using Clouds.Simulation;
using UnityEngine;

namespace Clouds.Rendering
{
    public class SimpleCloudRenderer : ACloudRenderer
    {
		[SerializeField]
		private Material _material;

		[SerializeField]
    	private Mesh _mesh;

        public override void Render(CloudGrid cloudGrid, float gridScale) {
            RenderParams rp = new(_material);
			Matrix4x4[] instancesData = new Matrix4x4[cloudGrid.Dimensions.x * cloudGrid.Dimensions.y * cloudGrid.Dimensions.z];
			int i = 0;
			for(int x = 0; x < cloudGrid.Dimensions.x; x++) {
				for(int y = 0; y < cloudGrid.Dimensions.y; y++) {
					for(int z = 0; z < cloudGrid.Dimensions.z; z++) {
						if(cloudGrid.GetCell(x, y, z).Cld) {
							instancesData[i++] = Matrix4x4.TRS(
									CalculateCellCenter(x, y, z, cloudGrid.Dimensions, gridScale),
									Quaternion.identity,
									gridScale * Vector3.one
							);
						}
					}
				}
			}
				
			Graphics.RenderMeshInstanced(rp, _mesh, 0, instancesData);
        }

		private Vector3 CalculateCellCenter(int x, int y, int z, Vector3Int gridDimensions, float gridScale) {
			return transform.position + gridScale * new Vector3(x, y, z) 
					- 0.5f * gridScale * (Vector3)gridDimensions + 0.5f * gridScale * Vector3.one;
		}
    }
}
