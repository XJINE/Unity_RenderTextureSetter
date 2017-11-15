using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureCameraSample : MonoBehaviour
{
    public RenderTextureCamera renderTextureCamera;

    protected void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(this.renderTextureCamera.RenderTexture, destination);
    }
}