using UnityEngine;

public class SettingsPopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private Animator popUpAnimator;
  
    private readonly int openHash = Animator.StringToHash("Open");
    private readonly int closeHash = Animator.StringToHash("Close");

    private bool isOpen = false;

    public void Show()
    {
        isOpen = true;
        popUpAnimator.SetTrigger(openHash);
    }

    public void Hide()
    {
        isOpen = false;
        popUpAnimator.SetTrigger(closeHash);    
    } 
    
    public void TogglePopUp()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
