using UnityEngine;
public class FitToSafeArea : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent(out RectTransform rectTransform))
        {
            Rect safeArea = Screen.safeArea;           

            Vector2 minAnchor = safeArea.position;
            Vector2 maxAnchor = minAnchor + safeArea.size;
            minAnchor.x /= Screen.width;
            minAnchor.y /= Screen.height;
            maxAnchor.x /= Screen.width;
            maxAnchor.y /= Screen.height;
            rectTransform.anchorMin = minAnchor;
            rectTransform.anchorMax = maxAnchor;
        }
    }
}