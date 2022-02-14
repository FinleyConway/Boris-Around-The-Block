using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private void Awake()
    {
        Button button = GetComponentInChildren<Button>();
        button.Select();
    }

    public void OnPlayButton()
    {
        MouseForcus.Instance.SetCursorState(true);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
