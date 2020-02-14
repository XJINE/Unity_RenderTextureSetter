using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetter : MonoBehaviour
{
    #region Field

    protected new Camera camera;

    [SerializeField]
    [FormerlySerializedAs("renderTextureSize")]
    protected Vector2Int size;

    [SerializeField]
    protected RenderTextureFormat format = RenderTextureFormat.ARGB32;

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

    public Vector2Int Size
    {
        get { return this.size; }
        set
        {
            this.size = value;
            InitializeTexture();
        }
    }

    public RenderTextureFormat Format
    {
        get { return this.format; }
        set
        {
            this.format = value;
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

        int width  = this.size.x <= 0 ? Screen.width  : this.size.x;
        int height = this.size.y <= 0 ? Screen.height : this.size.y;

        this.RenderTexture = new RenderTexture(width, height, 0, this.format);

        this.camera.targetTexture = this.RenderTexture;
    }

    protected virtual bool CheckRenderTextureSettingsIsValid()
    {
        // CAUTION:
        // Screen.width/height values are 0 when UnityEditor just started.
        // ExecuteInEditMode option will be enabled in same time.
        // RenderTexture not allow 0 width & height, so we need to ignore 0 values.

        return !(this.size.x <= 0 && (Screen.width == 0 || Screen.height == 0));
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