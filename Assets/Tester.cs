using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Tester : MonoBehaviour
{
    [SerializeField, HideInInspector] ComputeShader _compute;

    RenderTexture _tempTexture;

    void Start()
    {
        _tempTexture = new RenderTexture(4, 16, 0, RenderTextureFormat.ARGBHalf);
        _tempTexture.filterMode = FilterMode.Point;
        _tempTexture.enableRandomWrite = true;
        _tempTexture.Create();
    }

    void OnDestroy()
    {
        Destroy(_tempTexture);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        var kernel = _compute.FindKernel("Main");
        _compute.SetTexture(kernel, "TempTexture", _tempTexture);
        _compute.Dispatch(kernel, 1, 1, 1);

        Graphics.Blit(_tempTexture, destination);
    }
}
