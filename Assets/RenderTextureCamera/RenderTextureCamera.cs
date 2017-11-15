using UnityEngine;

/// <summary>
/// RenderTexture を設定するカメラ。
/// </summary>
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureCamera : MonoBehaviour
{
    #region Field

    /// <summary>
    /// キャッシュされたカメラ。
    /// </summary>
    protected new Camera camera;

    /// <summary>
    /// カメラに設定する RenderTexture 。
    /// </summary>
    protected RenderTexture renderTexture;

    /// <summary>
    /// RenderTexture の幅。-1 のとき Screen.Width になります。
    /// </summary>
    [SerializeField]
    protected int renderTextureWidth = -1;

    /// <summary>
    /// RenderTexture の高さ。-1 のとき Screen.height になります。
    /// </summary>
    [SerializeField]
    protected int renderTextureHeight = -1;

    #endregion Field

    #region Property

    /// <summary>
    /// RenderTexture を取得します。
    /// </summary>
    public RenderTexture RenderTexture
    {
        get { return this.renderTexture; }
    }

    /// <summary>
    /// RenderTexture の幅を取得、設定します。
    /// 設定時にカメラの情報も更新されます。
    /// </summary>
    public int RenderTextureWidth
    {
        get { return this.renderTextureWidth; }
        set
        {
            this.renderTextureWidth = value;
            InitializeTexture();
        }
    }

    /// <summary>
    /// RenderTexture の高さを取得、設定します。
    /// 設定時にカメラの情報も更新されます。
    /// </summary>
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

    /// <summary>
    /// 初期化時に呼び出されます。
    /// </summary>
    protected virtual void Awake()
    {
        this.camera = base.GetComponent<Camera>();
        InitializeTexture();
    }

    /// <summary>
    /// Inspector の更新時に呼び出されます。
    /// </summary>
    protected virtual void OnValidate()
    {
        if (this.camera != null)
        {
            InitializeTexture();
        }
    }

    /// <summary>
    /// テクスチャを初期化して設定します。
    /// テクスチャサイズを変更したときは必ず呼び出す必要があります。
    /// </summary>
    public virtual void InitializeTexture()
    {
        ReleaseTexture();

        int width  = this.renderTextureWidth  == -1 ? Screen.width  : this.renderTextureWidth;
        int height = this.renderTextureHeight == -1 ? Screen.height : this.renderTextureHeight;

        this.renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);

        SetTextureToCamera();
    }

    /// <summary>
    /// テクスチャリソースを解放します。
    /// </summary>
    protected virtual void ReleaseTexture()
    {
        this.camera.targetTexture = null;

        if (this.renderTexture != null)
        {
            GameObject.DestroyImmediate(this.renderTexture);
        }
    }

    /// <summary>
    /// テクスチャをカメラに設定します。
    /// </summary>
    protected virtual void SetTextureToCamera()
    {
        this.camera.targetTexture = this.renderTexture;
    }

    #endregion Method
}