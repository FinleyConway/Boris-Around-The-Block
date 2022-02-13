using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private AudioClip[] dialogueAudio;
    [SerializeField] private DialogueResponse[] responses;

    public string[] Dialogue => dialogue;

    public AudioClip[] DialogueAudio => dialogueAudio;

    public bool HasAudio => DialogueAudio != null && DialogueAudio.Length > 0;

    public DialogueResponse[] Responses => responses;

    public bool HasResponses => Responses != null && Responses.Length > 0;
}