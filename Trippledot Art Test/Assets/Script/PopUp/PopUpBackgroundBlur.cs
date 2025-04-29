using UnityEngine;
using UnityEngine.UI;

public class PopUpBackgroundBlur : MonoBehaviour
{
    [SerializeField] private RawImage blurredBackground;
    [SerializeField] private Material blurMat;

    private Camera cam;
    private RenderTexture capture;
    private RenderTexture blurPass;
    private int captureWidth;
    private int captureHeight;
    private const int resolutionDownsampleAmount = 4;

    private void Start()
    {
        cam = Camera.main;
        captureWidth = Screen.width / resolutionDownsampleAmount;
        captureHeight = Screen.height / resolutionDownsampleAmount;
    }

    private void OnEnable()
    {
        PopUp_OnOpen.OnPopUpOpen += CaptureCameraToRenderTexture;
        PopUp_OnClosed.OnPopUpClosed += ReleaseAllRT;
    }

    private void OnDisable()
    {
        PopUp_OnOpen.OnPopUpOpen -= CaptureCameraToRenderTexture;
        PopUp_OnClosed.OnPopUpClosed -= ReleaseAllRT;
    }

    private void ReleaseAllRT()
    {
        ReleaseRT(capture);
        ReleaseRT(blurPass);
    }

    private void ReleaseRT(RenderTexture rt)
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
        if (cam == null || blurredBackground == null || blurMat == null)
        {
            return;
        }

        ReleaseAllRT();

        capture = new RenderTexture(captureWidth, captureHeight, 0, RenderTextureFormat.ARGB32);
        capture.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;
        capture.useMipMap = false;
        capture.autoGenerateMips = false;
        capture.filterMode = FilterMode.Bilinear;

        capture.Create();
        cam.targetTexture = capture;
        cam.Render();
        cam.targetTexture = null;

        blurPass = new RenderTexture(capture);
        Graphics.Blit(capture, blurPass, blurMat);
        blurredBackground.texture = blurPass;

        ReleaseRT(capture);
    }
}
