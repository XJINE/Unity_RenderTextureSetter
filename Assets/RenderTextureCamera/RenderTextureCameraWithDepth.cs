using UnityEngine;

/// <summary>
/// Depth 用のテクスチャも設定する RenderTextureCamera 。
/// </summary>
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureCameraWithDepth : RenderTextureCamera
{ 
    #region Field

    /// <summary>
    /// Depth 用の renderTexture 。
    /// </summary>
    protected RenderTexture renderTextureDepth;

    #endregion Field

    #region Property

    /// <summary>
    /// Depth 用の RenderTexture を取得します。
    /// </summary>
    public RenderTexture RenderTextureDepth
    {
        get { return this.renderTextureDepth; }
    }

    #endregion Property

    #region Method

    /// <summary>
    /// テクスチャを初期化します。
    /// </summary>
    public override void InitializeTexture()
    {
        if (!CheckRenderTextureSettingsIsValid())
        {
            return;
        }

        ReleaseTexture();

        int width  = this.renderTextureWidth  <= 0 ? Screen.width  : this.renderTextureWidth;
        int height = this.renderTextureHeight <= 0 ? Screen.height : this.renderTextureHeight;

        base.renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        this.renderTextureDepth = new RenderTexture(width, height, 24, RenderTextureFormat.Depth);

        SetTextureToCamera();
    }

    /// <summary>
    /// テクスチャリソースを解放します。
    /// </summary>
    protected override void ReleaseTexture()
    {
        base.ReleaseTexture();

        if (this.renderTextureDepth != null)
        {
            GameObject.DestroyImmediate(this.renderTextureDepth);
        }
    }

    /// <summary>
    /// テクスチャをカメラに設定します。
    /// </summary>
    protected override void SetTextureToCamera()
    {
        this.camera.SetTargetBuffers(this.renderTexture.colorBuffer,
                                     this.renderTextureDepth.depthBuffer);
    }

    #endregion Method
}