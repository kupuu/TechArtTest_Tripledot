using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimatedToggle : Toggle
{
    [SerializeField] private RectTransform toggleDot;
    [SerializeField] private AnimationCurve dotCurve = AnimationCurve.EaseInOut(0,0,1,1);
    private Coroutine tweenCoroutine;

    protected override void Awake()
    {
        base.Awake();
        UpdateDotImmediate();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (toggleDot == null || toggleDot.parent == null)
        {
            return;
        }   
        
        if (tweenCoroutine != null)
        {
            StopCoroutine(tweenCoroutine);
        }

        tweenCoroutine = StartCoroutine(TweenDot(GetTargetPosition()));
    }

    public void UpdateDotImmediate()
    {
        if (toggleDot == null || toggleDot.parent == null)
        { 
            return;
        }

        toggleDot.anchoredPosition = GetTargetPosition();
    }

    private Vector2 GetTargetPosition()
    {
        float halfWidth = targetGraphic.rectTransform.rect.width * 0.5f;
        float dotHalfWidth = toggleDot.sizeDelta.x * 0.5f;
        float x = isOn ? (halfWidth - dotHalfWidth) : (-halfWidth + dotHalfWidth);

        return new Vector2(x, toggleDot.anchoredPosition.y);
    }

    private IEnumerator TweenDot(Vector2 newPos)
    {
        Vector2 startPos = toggleDot.anchoredPosition;
        Timer timer = new Timer(colors.fadeDuration);

        while (!timer.IsCompleted)
        {
            toggleDot.anchoredPosition = Vector2.Lerp(startPos, newPos, dotCurve.Evaluate(timer.Progress));
            yield return null;
        }

        toggleDot.anchoredPosition = newPos;
    }

}
