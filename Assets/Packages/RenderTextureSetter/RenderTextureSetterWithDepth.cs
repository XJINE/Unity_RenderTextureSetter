using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetterWithDepth : RenderTextureSetter
{ 
    #region Property

    public RenderTexture RenderTextureDepth
    {
        get;
        protected set;
    }

    #endregion Property

    #region Method

    public override void InitializeTexture()
    {
        if (!base.IsInitialized)
        {
            Initialize();
            return;
        }

        if (!CheckRenderTextureSettingsIsValid())
        {
            return;
        }

        ReleaseTexture();

        int width  = this.renderTextureSize.x <= 0 ? Screen.width  : this.renderTextureSize.x;
        int height = this.renderTextureSize.y <= 0 ? Screen.height : this.renderTextureSize.y;

        base.RenderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        this.RenderTextureDepth = new RenderTexture(width, height, 24, RenderTextureFormat.Depth);

        this.camera.SetTargetBuffers(base.RenderTexture.colorBuffer,
                                     this.RenderTextureDepth.depthBuffer);
    }

    protected override void ReleaseTexture()
    {
        base.ReleaseTexture();

        if (this.RenderTextureDepth != null)
        {
            GameObject.DestroyImmediate(this.RenderTextureDepth);
        }
    }

    #endregion Method
}