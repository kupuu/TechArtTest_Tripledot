using UnityEngine;

public class SettingsPopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private Animator popUpAnimator;  
    private readonly int isOpenHash = Animator.StringToHash("isOpen");
    private bool isOpen = false;

    public void Show()
    {
        isOpen = true;
        popUpAnimator.SetBool(isOpenHash, isOpen);
    }

    public void Hide()
    {
        isOpen = false;
        popUpAnimator.SetBool(isOpenHash, isOpen);    
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
