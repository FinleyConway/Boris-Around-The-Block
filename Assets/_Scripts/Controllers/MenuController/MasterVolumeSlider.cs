using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(val => SoundSystem.Instance.ChangeMasterVolume(val));
    }
}
