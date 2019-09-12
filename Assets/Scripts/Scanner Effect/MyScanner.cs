using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(ScannerEffectRenderer), PostProcessEvent.BeforeStack, "Custom/Scanner Effect")]
public sealed class ScannerEffect : PostProcessEffectSettings
{
    //public RenderTextureParameter depthTexture;

}

public sealed class ScannerEffectRenderer : PostProcessEffectRenderer<ScannerEffect>
{
    public override DepthTextureMode GetCameraFlags()
    {
        return DepthTextureMode.Depth;
    }

    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/DepthShader"));
        //sheet.properties.SetFloat("_Blend", settings.blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}

[Serializable]
public sealed class RenderTextureParameter : ParameterOverride<RenderTexture> { }
