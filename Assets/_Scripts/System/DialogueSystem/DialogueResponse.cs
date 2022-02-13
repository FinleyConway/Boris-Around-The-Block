using UnityEngine;

[System.Serializable]
public class DialogueResponse
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueData dialogueData;

    public string ResponseText => responseText;
    public DialogueData DialogueData => dialogueData;
}
