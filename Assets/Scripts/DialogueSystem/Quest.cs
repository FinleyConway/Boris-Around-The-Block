using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private string stateToWatch;
    private int volume;

    private void Start()
    {
        if (DialogueManager.instance.currentStory != null)
            DialogueManager.instance.currentStory.ObserveVariable(stateToWatch, Karma);
    }

    private void Karma(string varName, object newValue)
    {
        volume = (int)newValue;
        Debug.Log(volume);
    }
}
