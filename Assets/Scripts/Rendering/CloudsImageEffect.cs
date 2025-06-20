using UnityEngine;

namespace Clouds.Rendering
{
	[ExecuteInEditMode, ImageEffectAllowedInSceneView]
	public class CloudsImageEffect : MonoBehaviour
	{
		[SerializeField]
		private Shader _shader;

		[SerializeField]
		private Transform _container;

		private Material _material;

		[ImageEffectOpaque]
    	private void OnRenderImage (RenderTexture src, RenderTexture dest) {
			if (_material == null || _material.shader != _shader) {
				_material = new Material (_shader);
			}

			_material.SetVector ("BoundsMin", _container.position - _container.localScale / 2);
        	_material.SetVector ("BoundsMax", _container.position + _container.localScale / 2);

			Graphics.Blit(src, dest, _material);
		}
	}
}
