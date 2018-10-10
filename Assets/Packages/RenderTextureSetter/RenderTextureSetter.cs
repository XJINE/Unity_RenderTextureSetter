using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetter : MonoBehaviour
{
    #region Field

    protected new Camera camera;

    [SerializeField]
    protected Vector2Int renderTextureSize;

    #endregion Field

    #region Property

    public bool IsInitialized
    {
        get;
        protected set;
    }

    public RenderTexture RenderTexture
    {
        get;
        protected set;
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
        Initialize();
    }

    protected virtual void OnValidate()
    {
        if (!Initialize())
        {
            InitializeTexture();
        }
    }

    public virtual bool Initialize()
    {
        if (this.IsInitialized)
        {
            return false;
        }
        
        this.IsInitialized = true;

        this.camera = base.GetComponent<Camera>();

        InitializeTexture();

        return true;
    }

    public virtual void InitializeTexture()
    {
        if (!this.IsInitialized)
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

        this.RenderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        this.camera.targetTexture = this.RenderTexture;
    }

    protected virtual bool CheckRenderTextureSettingsIsValid()
    {
        // CAUTION:
        // Screen.width/height values are 0 when UnityEditor just started.
        // ExecuteInEditMode option will be enabled in same time.
        // RenderTexture not allow 0 width & height, so we need to ignore 0 values.

        return !(this.renderTextureSize.x <= 0 && (Screen.width == 0 || Screen.height == 0));
    }

    protected virtual void ReleaseTexture()
    {
        this.camera.targetTexture = null;

        if (this.RenderTexture != null)
        {
            GameObject.DestroyImmediate(this.RenderTexture);
        }
    }

    #endregion Method
}