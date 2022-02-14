using UnityEngine;

public class MouseForcus : Singleton<MouseForcus>
{
    // hides cursor when game is in focus
    public void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

    private void OnApplicationFocus(bool focus)
    {
        //SetCursorState(focus);
    }
}
