using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetter : MonoBehaviour
{
    #region Field

    protected new Camera camera;

    protected RenderTexture renderTexture;

    [SerializeField]
    protected Vector2Int renderTextureSize;

    #endregion Field

    #region Property

    public RenderTexture RenderTexture
    {
        get { return this.renderTexture; }
    }

    public Vector2Int RenderTextureSize 
    {
        get { return this.renderTextureSize; }
        set
        {
            this.renderTextureSize = value;
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

        int width  = this.renderTextureSize.x <= 0 ? Screen.width  : this.renderTextureSize.x;
        int height = this.renderTextureSize.y <= 0 ? Screen.height : this.renderTextureSize.y;

        this.renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        this.camera.targetTexture = this.renderTexture;
    }

    protected virtual bool CheckRenderTextureSettingsIsValid()
    {
        // CAUTION:
        // Screen.width & Screen.height values are 0 when UnityEditor just started.
        // ExecuteInEditMode option will be enabled in same time.
        // RenderTexture not allow 0 width & height, so we need to ignore 0 values.

        return !(this.renderTextureSize.x <= 0 && (Screen.width == 0 || Screen.height == 0));
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