using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RenderTextureSetterSample : MonoBehaviour
{
    public RenderTextureSetter renderTextureSetter;

    protected void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(this.renderTextureSetter.RenderTexture, destination);
    }
}