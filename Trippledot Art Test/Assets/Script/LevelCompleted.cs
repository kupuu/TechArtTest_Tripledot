using System.Collections;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatedTotal star;
    [SerializeField] private AnimatedTotal coin;
    [SerializeField] private AnimatedTotal crown;
    private Coroutine animatedTotalsRoutine = null;
    private readonly int isOpenHash = Animator.StringToHash("isOpen");
    private readonly WaitForSeconds delay = new WaitForSeconds(0.25f);

    private void OnEnable()
    {
        LevelCompleted_OnOpen.OnLevelCompletedScreenOpened += AnimatedTotals;
    }

    private void OnDisable()
    {
        LevelCompleted_OnOpen.OnLevelCompletedScreenOpened -= AnimatedTotals;
    }

    public void ShowLevelCompleted()
    {
        star.SetTotal(0);
        coin.SetTotal(0);
        crown.SetTotal(0);
        animator.SetBool(isOpenHash, true);
    }

    public void AnimatedTotals()
    {
        if (animatedTotalsRoutine != null)
        {
            StopCoroutine(animatedTotalsRoutine);
        }
        animatedTotalsRoutine = StartCoroutine(AimatedTotals());
    }

    private IEnumerator AimatedTotals()
    {        
        yield return star.PlayTotal(250);        
        yield return coin.PlayTotal(100);
        yield return crown.PlayTotal(8);
        animatedTotalsRoutine = null;
    }

    public void HideLevelCompleted()
    {
        if(animatedTotalsRoutine != null)
        {
            StopCoroutine(animatedTotalsRoutine);
        }
        animator.SetBool(isOpenHash, false);
    }
}
