using System;
using UnityEngine;

public class PopUp_OnOpen : StateMachineBehaviour
{
    public static event Action OnPopUpOpen = delegate { };
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnPopUpOpen?.Invoke();
    }
}
