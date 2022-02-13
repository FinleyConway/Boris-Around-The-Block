using System;
using UnityEngine;

public class DialogueResponseEvent : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private ResponseEvent[] events;

    public DialogueData DialogueData => dialogueData;

    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if (dialogueData == null)
            return;
        if (dialogueData.Responses == null)
            return;
        if (events != null && events.Length == dialogueData.Responses.Length)
            return;

        if (events == null)
        {
            events = new ResponseEvent[dialogueData.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueData.Responses.Length);
        }

        for (int i = 0; i < dialogueData.Responses.Length; i++)
        {
            DialogueResponse dialogueResponse = dialogueData.Responses[i];

            if (events[i] != null)
            {
                events[i].name = dialogueResponse.ResponseText;
                continue;
            }

            events[i] = new ResponseEvent() { name = dialogueResponse.ResponseText };
        }
    }
}
