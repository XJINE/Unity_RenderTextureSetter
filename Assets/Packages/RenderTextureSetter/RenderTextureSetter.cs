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

    [SerializeField] protected Vector2Int          size         = new (1028, 1028);
    [SerializeField] protected RenderTextureFormat format       = RenderTextureFormat.ARGB32;
    [SerializeField] protected AntiAliasingLevel   antiAliasing = AntiAliasingLevel.x1;
    [SerializeField] protected DepthLevel          depth        = DepthLevel.x0;

    #endregion Field

    #region Property

    public bool          IsInitialized { get; protected set; }
    public RenderTexture RenderTexture { get; protected set; }

    public Vector2Int Size
    {
        get => size;
        set { size = value; InitializeTexture(); }
    }

    public RenderTextureFormat Format
    {
        get => format;
        set { format = value; InitializeTexture(); }
    }

    public AntiAliasingLevel AntiAliasing
    {
        get => antiAliasing;
        set { antiAliasing = value; InitializeTexture(); }
    }

    public DepthLevel Depth
    {
        get => depth;
        set { depth = value; InitializeTexture(); }
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
        IsInitialized = true;

        camera = GetComponent<Camera>();

        InitializeTexture();

        return true;
    }

    protected virtual void InitializeTexture()
    {
        ReleaseTexture();

        var width  = size.x <= 0 ? Screen.width  : size.x;
        var height = size.y <= 0 ? Screen.height : size.y;

        RenderTexture = new RenderTexture(width, height, 0, format)
        {
            antiAliasing = (int)antiAliasing,
            depth        = (int)depth
        };

        camera.targetTexture = RenderTexture;
    }

    protected virtual void ReleaseTexture()
    {
        camera.targetTexture = null;

        if (RenderTexture != null)
        {
            DestroyImmediate(RenderTexture);
            RenderTexture = null;
        }
    }

    #endregion Method
}