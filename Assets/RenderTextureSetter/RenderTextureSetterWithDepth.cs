using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetterWithDepth : RenderTextureSetter
{ 
    #region Field

    protected RenderTexture renderTextureDepth;

    #endregion Field

    #region Property

    public RenderTexture RenderTextureDepth
    {
        get { return this.renderTextureDepth; }
    }

    #endregion Property

    #region Method

    public override void InitializeTexture()
    {
        if (!CheckRenderTextureSettingsIsValid())
        {
            return;
        }

        ReleaseTexture();

        int width  = this.renderTextureSize.x <= 0 ? Screen.width  : this.renderTextureSize.x;
        int height = this.renderTextureSize.y <= 0 ? Screen.height : this.renderTextureSize.y;

        this.renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        this.renderTextureDepth = new RenderTexture(width, height, 24, RenderTextureFormat.Depth);

        this.camera.SetTargetBuffers(this.renderTexture.colorBuffer,
                                     this.renderTextureDepth.depthBuffer);
    }

    protected override void ReleaseTexture()
    {
        base.ReleaseTexture();

        if (this.renderTextureDepth != null)
        {
            GameObject.DestroyImmediate(this.renderTextureDepth);
        }
    }

    #endregion Method
}