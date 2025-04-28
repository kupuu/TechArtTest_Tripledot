using UnityEngine;
using UnityEngine.UI;

public class PopUpBackgroundBlur : MonoBehaviour
{
    [SerializeField] private RawImage blurredBackground;

    private Camera cam;
    private RenderTexture rt;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        PopUp_OnOpen.OnPopUpOpen += CaptureCameraToRenderTexture;
    }

    private void OnDisable()
    {
        PopUp_OnOpen.OnPopUpOpen -= CaptureCameraToRenderTexture;
        ReleaseCapturedRenderTexture();
    }

    private void ReleaseCapturedRenderTexture()
    {
        if (rt != null)
        {
            if (rt.IsCreated())
            {
                rt.Release();
            }
            Destroy(rt);
            rt = null;
        }
    }

    private void CaptureCameraToRenderTexture()
    {
        if (cam == null || blurredBackground == null)
        {
            return;
        }

        ReleaseCapturedRenderTexture();

        rt = new RenderTexture(Screen.width / 4, Screen.height / 4, 0, RenderTextureFormat.ARGB32);
        rt.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;
        rt.useMipMap = false;
        rt.autoGenerateMips = false;
        rt.filterMode = FilterMode.Bilinear;

        rt.Create();
        cam.targetTexture = rt;
        cam.Render();
        cam.targetTexture = null;
        blurredBackground.texture = rt;
    }
}
