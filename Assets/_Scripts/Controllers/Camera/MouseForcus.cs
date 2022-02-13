using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseForcus : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    private void Update()
    {
        if (dialogueUI.IsOpen)
            OnApplicationFocus(false);
        else
            OnApplicationFocus(true);
    }

    // hides cursor when game is in focus
    public void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

    private void OnApplicationFocus(bool focus)
    {
        SetCursorState(focus);
    }
}
