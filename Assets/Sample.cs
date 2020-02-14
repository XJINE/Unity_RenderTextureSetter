using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteAlways]
public class Sample : MonoBehaviour
{
    public RenderTextureSetter renderTextureSetter;

    protected void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(this.renderTextureSetter.RenderTexture, destination);
    }
}