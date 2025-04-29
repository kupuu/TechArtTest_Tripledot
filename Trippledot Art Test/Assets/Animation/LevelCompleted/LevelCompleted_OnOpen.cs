using System;
using UnityEngine;

public class LevelCompleted_OnOpen : StateMachineBehaviour
{
    public static event Action OnLevelCompletedScreenOpened = delegate { };

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnLevelCompletedScreenOpened?.Invoke();
    }
}
