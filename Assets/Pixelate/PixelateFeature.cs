using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelateFeature : ScriptableRendererFeature
{
    class PixelatePass : ScriptableRenderPass
    {
        private RenderTargetIdentifier source { get; set; }
        private RenderTargetHandle destination { get; set; }
        private string profilerTag = "_PixelatePass";
        private Material material;
        RenderTargetHandle temporaryColorTexture;

        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public PixelatePass(Material material)
        {
            this.material = material;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {

        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

            RenderTextureDescriptor opaqueDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDescriptor.depthBufferBits = 0;

            if (destination == RenderTargetHandle.CameraTarget)
            {
                cmd.GetTemporaryRT(temporaryColorTexture.id, opaqueDescriptor, FilterMode.Point);
                Blit(cmd, source, temporaryColorTexture.Identifier(), material, 0);
                Blit(cmd, temporaryColorTexture.Identifier(), source);

            }
            else Blit(cmd, source, destination.Identifier(), material, 0);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (destination == RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(temporaryColorTexture.id);
        }
    }

    [System.Serializable]
    public class PixelateSettings
    {
        public Material material = null;
    }

    public PixelateSettings settings = new PixelateSettings();
    PixelatePass pixelatePass;
    RenderTargetHandle pixelateTexture;

    public override void Create()
    {
        pixelatePass = new PixelatePass(settings.material);
        pixelatePass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        pixelateTexture.Init("_PixelateTexture");
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.material == null)
        {
            Debug.LogWarningFormat("Missing pixelate material");
            return;
        }
        pixelatePass.Setup(renderer.cameraColorTarget, RenderTargetHandle.CameraTarget);
        renderer.EnqueuePass(pixelatePass);
    }
}

