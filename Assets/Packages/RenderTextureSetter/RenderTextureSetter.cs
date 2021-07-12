using UnityEngine;

[RequireComponent(typeof(Camera))][ExecuteAlways]
public class RenderTextureSetter : MonoBehaviour, IInitializable
{
    public enum AntiAliasingLevel
    {
        x1 = 1,
        x2 = 2,
        x4 = 4,
        x8 = 8,
    }

    public enum DepthLevel
    {
         x0 =  0,
        x16 = 16,
        x24 = 24,
        x32 = 32
    }

    #region Field

    protected new Camera camera;

    [SerializeField] protected Vector2Int          size         = new Vector2Int(1028, 1028);
    [SerializeField] protected RenderTextureFormat format       = RenderTextureFormat.ARGB32;
    [SerializeField] protected AntiAliasingLevel   antiAliasing = AntiAliasingLevel.x1;
    [SerializeField] protected DepthLevel          depth        = DepthLevel.x0;

    #endregion Field

    #region Property

    public bool          IsInitialized { get; protected set; }
    public RenderTexture RenderTexture { get; protected set; }

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

    public AntiAliasingLevel AntiAliasing
    {
        get { return this.antiAliasing; }
        set
        {
            this.antiAliasing = value;
            InitializeTexture();
        }
    }

    public DepthLevel Depth
    {
        get { return this.depth; }
        set
        {
            this.depth= value;
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
        Initialize();
    }

    public virtual bool Initialize()
    {
        this.IsInitialized = true;

        this.camera = base.GetComponent<Camera>();

        InitializeTexture();

        return true;
    }

    protected virtual void InitializeTexture()
    {
        ReleaseTexture();

        int width  = this.size.x <= 0 ? Screen.width  : this.size.x;
        int height = this.size.y <= 0 ? Screen.height : this.size.y;

        this.RenderTexture = new RenderTexture(width, height, 0, this.format)
        {
            antiAliasing = (int)this.antiAliasing,
            depth        = (int)this.depth
        };

        this.camera.targetTexture = this.RenderTexture;
    }

    protected virtual void ReleaseTexture()
    {
        this.camera.targetTexture = null;

        if (this.RenderTexture != null)
        {
            DestroyImmediate(this.RenderTexture);
            this.RenderTexture = null;
        }
    }

    #endregion Method
}