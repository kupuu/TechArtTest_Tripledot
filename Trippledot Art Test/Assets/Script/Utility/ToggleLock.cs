using UnityEngine;

public class ToggleLock : MonoBehaviour
{
    [SerializeField] private BottomBarView bottomBarView;
    private bool isLocked = true;

    public void ToggleLockTesting()
    {
        isLocked = !isLocked;
        if (isLocked)
        {
            bottomBarView.LockButtonByIndex(0);
        }
        else
        {
            bottomBarView.UnlockButtonByIndex(0);
        }       
    }

}
