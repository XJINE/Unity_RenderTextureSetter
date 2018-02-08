using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetterSample : MonoBehaviour
{
    public RenderTextureSetter renderTextureCamera;

    protected void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(this.renderTextureCamera.RenderTexture, destination);
    }
}