using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarButton : MonoBehaviour
{
    [SerializeField] private bool isLocked = false;
    private bool isSelected = false;

    [Header("Button components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image highlight;
    [SerializeField] private Image icon;
    [SerializeField] private Image lockIcon;

    [Space(6)]
    [Header("Animation Settings")]
    [SerializeField] private float selectionDuration = 0.2f;
    [SerializeField] private float highlightRise = 30f;
    [SerializeField] private float iconRise = 30f;
    [SerializeField] private float highlightWidth = 600f;
    [SerializeField] private float defaultHighlightWidth = 300f;
    [SerializeField] private Vector3 lockScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private AnimationCurve smoothCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve bounceCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);      
    
    private Coroutine selectionRoutine = null;
    private Vector2 defaultIconPos;
    private RectTransform rect;    

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        defaultIconPos = icon.rectTransform.localPosition;

        if (isLocked)
        {
            icon.rectTransform.localScale = Vector3.zero;
            lockIcon.rectTransform.localScale = lockScale;
            button.interactable = false;
            button.image.raycastTarget = false;
        }
        else
        {
            icon.rectTransform.localScale = Vector3.one;
            lockIcon.rectTransform.localScale = Vector3.zero;
            button.interactable = true;
            button.image.raycastTarget = true;

        }
    }   

    public void SetSelection(bool isSelected)
    {
        if(this.isSelected == isSelected)
        {
            return;
        }

        this.isSelected = isSelected;

        if (selectionRoutine != null)
        {
            StopCoroutine(selectionRoutine);
        }
        selectionRoutine = StartCoroutine(SetSelectionRoutine());
    }

    public void Lock()
    {
        if (isLocked)
        {
            return;
        }

        isLocked = true;
        StartCoroutine(LockRoutine());                
    }

    public void Unlock()
    {
        if (!isLocked)
        {
            return;
        }
        
        isLocked = false; 
        StartCoroutine(UnlockRoutine());
    }     

    private IEnumerator LockRoutine()
    {
        button.interactable = false;
        button.image.raycastTarget = false;
        yield return AnimateScale(icon.rectTransform, Vector3.zero, 0.1f, smoothCurve);
        yield return AnimateScale(lockIcon.rectTransform, lockScale, 0.2f, bounceCurve);        
    }

    private IEnumerator UnlockRoutine()
    {
        yield return AnimateScale(lockIcon.rectTransform, Vector3.zero, 0.1f, smoothCurve);
        yield return AnimateScale(icon.rectTransform, Vector3.one, 0.2f, bounceCurve);
        button.interactable = true;
        button.image.raycastTarget = true;
    }

    private IEnumerator AnimateScale(RectTransform target, Vector3 targetScale, float duration, AnimationCurve curve)
    {
        Timer timer = new (duration);
        Vector3 currentScale = target.localScale;

        while (!timer.IsCompleted)
        {
            float t = curve.Evaluate(timer.Progress);
            target.localScale = Vector3.LerpUnclamped(currentScale, targetScale, t);
            yield return null;
        }
        
        target.localScale = targetScale;
    }

    private IEnumerator SetSelectionRoutine()
    {
        Timer timer = new(selectionDuration);

        highlight.enabled = isSelected;
        buttonText.enabled = isSelected;

        Vector2 currentRectSize = rect.sizeDelta;
        float targetWidth = isSelected ? highlightWidth : defaultHighlightWidth;
        Vector2 targetRectSize = new Vector2(targetWidth, currentRectSize.y);

        Vector2 highlightOffsetMax = highlight.rectTransform.offsetMax;
        float targetOffsetMaxY = isSelected ? highlightRise : 0;
        Vector2 targetOffset = new Vector2(highlightOffsetMax.x, targetOffsetMaxY);

        Vector2 buttonIconPos = icon.rectTransform.localPosition;
        float targetPosY = isSelected ? iconRise : defaultIconPos.y;
        Vector2 targetButtonIconPos = new Vector2(buttonIconPos.x, targetPosY);        

        while (!timer.IsCompleted)
        {
            float t = bounceCurve.Evaluate(timer.Progress);
            highlight.rectTransform.offsetMax = Vector2.LerpUnclamped(highlightOffsetMax, targetOffset, t);
            rect.sizeDelta = Vector2.LerpUnclamped(currentRectSize, targetRectSize, t);
            icon.rectTransform.localPosition = Vector2.Lerp(buttonIconPos, targetButtonIconPos, t);
            yield return null;
        }

        highlight.rectTransform.offsetMax = targetOffset;
        rect.sizeDelta = targetRectSize;
        icon.rectTransform.localPosition = targetButtonIconPos;

        selectionRoutine = null;
    }
}
