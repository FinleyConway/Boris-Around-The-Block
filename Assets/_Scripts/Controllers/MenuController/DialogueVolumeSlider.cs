using UnityEngine;
using UnityEngine.UI;

public class DialogueVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(val => SoundSystem.Instance.ChangeDialogueVolume(val));
    }
}
