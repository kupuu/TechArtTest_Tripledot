using System;
using UnityEngine;

public class BottomBarView : MonoBehaviour
{
    public static event Action ContentActivated = delegate { };
    public static event Action Closed = delegate { };

    [SerializeField] private RectTransform buttonContainer;
    [SerializeField] private Animator bottomBarAnimator;
    private readonly int bottomBarVisibilityHash = Animator.StringToHash("BottomBarVisible");
    private BottomBarButton[] barButtons;
    private int lastSelectedIndex = -1;

    private void Awake()
    {
        barButtons = buttonContainer.GetComponentsInChildren<BottomBarButton>();
    }

    public void HideBottomBar()
    {
        bottomBarAnimator.SetBool(bottomBarVisibilityHash, false);
    }

    public void ShowBottomBar()
    {
        bottomBarAnimator.SetBool(bottomBarVisibilityHash, true);
    }   
    
    public void ToggleBottomBar()
    {
        bottomBarAnimator.SetBool(bottomBarVisibilityHash, !bottomBarAnimator.GetBool(bottomBarVisibilityHash));       
    }

    public void LockButtonByIndex(int buttonIndex)
    {
        if (lastSelectedIndex == buttonIndex)
        {
            barButtons[lastSelectedIndex].SetSelection(false);
            lastSelectedIndex = -1;
            Closed?.Invoke();
        }
        barButtons[buttonIndex].Lock();
    }

    public void UnlockButtonByIndex(int buttonIndex)
    {
        barButtons[buttonIndex].Unlock();
    }

    public void ButtonClickedByIndex(int buttonIndex)
    {
        if(lastSelectedIndex == buttonIndex)
        {
            barButtons[lastSelectedIndex].SetSelection(false);
            lastSelectedIndex = -1;
            Closed?.Invoke();
            return;
        }

        if(lastSelectedIndex >= 0)
        {
            barButtons[lastSelectedIndex].SetSelection(false);
        }

        barButtons[buttonIndex].SetSelection(true);
        lastSelectedIndex = buttonIndex;
        ContentActivated?.Invoke();  
    }
}



