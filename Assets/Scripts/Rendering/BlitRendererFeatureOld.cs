using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

#pragma warning disable 0612, 0618 //Disable obsolete warning
public class BlitRendererFeatureOld : ScriptableRendererFeature
{

    [Serializable]
    private class Settings 
    {
        public Material Material;
        public RenderPassEvent RenderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    [Obsolete]
    private class RenderPass : ScriptableRenderPass
    {
        private ProfilingSampler _profilingSampler;
        private string _name;
        private Material _material;
        public RenderTargetIdentifier CameraColorTarget {private get; set;}
        private RenderTargetHandle _tempTextureHandle;

        public RenderPass(string name, Material material)
        {
            _name = name;
            _material = material;
            _profilingSampler = new(_name);
            _tempTextureHandle.Init("_TempBlitMaterialTexture");
        }

        [Obsolete]
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if(_material == null) return;

            CommandBuffer cmd = CommandBufferPool.Get();

            RenderTextureDescriptor cameraTextureDesc = renderingData.cameraData.cameraTargetDescriptor;
            cameraTextureDesc.depthBufferBits = 0;
            cmd.GetTemporaryRT(_tempTextureHandle.id, cameraTextureDesc, FilterMode.Bilinear);

            using (new ProfilingScope(cmd, _profilingSampler)) {
                Blit(cmd, CameraColorTarget, _tempTextureHandle.Identifier(), _material, 0);
                Blit(cmd, _tempTextureHandle.Identifier(), CameraColorTarget);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    [SerializeField]
    private Settings _settings;
    private RenderPass _renderPass;

    public override void Create()
    {
        _renderPass = new RenderPass(name, _settings.Material);
        _renderPass.renderPassEvent = _settings.RenderPassEvent;
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        _renderPass.CameraColorTarget = renderer.cameraColorTargetHandle;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_renderPass);
    }
}
#pragma warning restore 0612, 0618
