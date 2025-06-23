using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using static UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils;

public class CloudsRendererFeature : ScriptableRendererFeature
{
    class CloudsRenderPass : ScriptableRenderPass
    {
        const string _passName = "CloudsPass";
        private Material _blitMaterial;
        public void Setup(Material material) {
            _blitMaterial = material;
            requiresIntermediateTexture = true;
        }

        // RecordRenderGraph is where the RenderGraph handle can be accessed, through which render passes can be added to the graph.
        // FrameData is a context container through which URP resources can be accessed and managed.
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();

            if(resourceData.isActiveTargetBackBuffer) {
                Debug.LogError("Skipping render pass. ditherEffectRendererFeature requires an intermediate ColorTexture, we can't use the BackBuffer as a texture input.");
                return;
            }

            TextureHandle source = resourceData.activeColorTexture;

            TextureDesc destinationDesc = renderGraph.GetTextureDesc(source);
            destinationDesc.name = $"CameraColor-{_passName}";
            destinationDesc.clearBuffer = false;

            TextureHandle destination = renderGraph.CreateTexture(destinationDesc);
            BlitMaterialParameters blitParameters = new(source, destination, _blitMaterial, 0);
            renderGraph.AddBlitPass(blitParameters, passName: _passName);

            resourceData.cameraColor = destination;
        }
    }

    [SerializeField]
    private RenderPassEvent _renderPassEvent = RenderPassEvent.AfterRenderingOpaques;

    [SerializeField]
    private Material _cloudsMaterial;

    private CloudsRenderPass _scriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        _scriptablePass = new();
        _scriptablePass.renderPassEvent = _renderPassEvent;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if(_cloudsMaterial == null) {
            Debug.LogWarning("CloudsRendererFeature material is null and will be skipped.");
            return;
        }

        _scriptablePass.Setup(_cloudsMaterial);
        renderer.EnqueuePass(_scriptablePass);
    }
}
