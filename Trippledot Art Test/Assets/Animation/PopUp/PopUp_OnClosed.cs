using System;
using UnityEngine;

public class PopUp_OnClosed : StateMachineBehaviour
{
    public static event Action OnPopUpClosed = delegate { };
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnPopUpClosed?.Invoke();
    }
}
