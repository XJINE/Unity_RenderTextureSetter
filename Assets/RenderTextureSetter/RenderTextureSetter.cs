using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetter : MonoBehaviour
{
    #region Field

    protected new Camera camera;

    protected RenderTexture renderTexture;

    [SerializeField]
    protected int renderTextureWidth = 0;

    [SerializeField]
    protected int renderTextureHeight = 0;

    #endregion Field

    #region Property

    public RenderTexture RenderTexture
    {
        get { return this.renderTexture; }
    }

    public int RenderTextureWidth
    {
        get { return this.renderTextureWidth; }
        set
        {
            this.renderTextureWidth = value;
            InitializeTexture();
        }
    }

    public int RenderTextureHeight
    {
        get { return this.renderTextureHeight; }
        set
        {
            this.renderTextureHeight = value;
            InitializeTexture();
        }
    }

    #endregion Property

    #region Method

    protected virtual void Awake()
    {
        this.camera = base.GetComponent<Camera>();
        InitializeTexture();
    }

    protected virtual void OnValidate()
    {
        if (this.camera != null)
        {
            InitializeTexture();
        }
    }

    public virtual void InitializeTexture()
    {
        if (!CheckRenderTextureSettingsIsValid())
        {
            return;
        }

        ReleaseTexture();

        int width  = this.renderTextureWidth  <= 0 ? Screen.width  : this.renderTextureWidth;
        int height = this.renderTextureHeight <= 0 ? Screen.height : this.renderTextureHeight;

        this.renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        this.camera.targetTexture = this.renderTexture;
    }

    protected virtual bool CheckRenderTextureSettingsIsValid()
    {
        // CAUTION:
        // Screen.width & Screen.height values are 0 when UnityEditor just started.
        // ExecuteInEditMode option will be enabled in same time.
        // RenderTexture not allow 0 width & height, so we need to ignore 0 values.

        return !(this.renderTextureWidth <= 0 && (Screen.width == 0 || Screen.height == 0));
    }

    protected virtual void ReleaseTexture()
    {
        this.camera.targetTexture = null;

        if (this.renderTexture != null)
        {
            GameObject.DestroyImmediate(this.renderTexture);
        }
    }

    #endregion Method
}