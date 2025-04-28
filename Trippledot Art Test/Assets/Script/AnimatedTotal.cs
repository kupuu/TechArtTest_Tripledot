using System.Collections;
using TMPro;
using UnityEngine;

public class AnimatedTotal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI animatedText;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private ParticleSystem particle;

    public void SetTotal(int total)
    {
        animatedText.text = total.ToString();
    }

    public IEnumerator PlayTotal(int total)
    {        
        int lastDisplayed = 0;
        Timer timer = new(duration);
        
        if (particle)
        {
            particle.Play();
        }        

        while (!timer.IsCompleted)
        {
            float t = timer.Progress;
            float animatedTotal = Mathf.Lerp(0, total, t);
            int displayedTotal = Mathf.RoundToInt(animatedTotal);

            if (displayedTotal != lastDisplayed)
            {
                animatedText.text = displayedTotal.ToString();
                lastDisplayed = displayedTotal;
            }

            animatedText.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, curve.Evaluate(t));

            yield return null;
        }
        
        animatedText.text = total.ToString();

        if (particle)
        {
            particle.Stop();
        }        
    }
}
