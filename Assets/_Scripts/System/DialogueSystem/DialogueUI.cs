using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueTextArea;
    [SerializeField] private TMP_Text dialogueText;

    public bool IsOpen { get; private set; }

    private DialogueResponseHandler dialogueResponseHandler;
    private TypeWriterEffect typeWriterEffect;
    private InputSystem inputSystem;
    private SoundSystem soundSystem;

    private void Awake()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        dialogueResponseHandler = GetComponent<DialogueResponseHandler>();
        inputSystem = InputSystem.Instance;
        soundSystem = SoundSystem.Instance;
        CloseDialogueBox();
    }

    // opens and starts convo
    public void ShowDialogue(DialogueData dialogueData)
    {
        IsOpen = true;
        dialogueTextArea.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueData));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        dialogueResponseHandler.AddResponseEvents(responseEvents);
    }

    // handles the logic of continuation of the convo and handles if theres any choices that need to be delt with
    private IEnumerator StepThroughDialogue(DialogueData dialogueData)
    {
        for (int i = 0; i < dialogueData.Dialogue.Length; i++)
        {
            string dialogue = dialogueData.Dialogue[i];

            if (dialogueData.HasAudio)
            {
                soundSystem.PlayDialogueSound(dialogueData.DialogueAudio[i]);
            }

            yield return RunTypingEffect(dialogue);

            dialogueText.text = dialogue;

            if (i == dialogueData.Dialogue.Length - 1 && dialogueData.HasResponses)
            {
                break;
            }

            yield return null;
            yield return new WaitUntil(() => inputSystem.OnSelect);
        }

        if (dialogueData.HasResponses)
        {
            dialogueResponseHandler.ShowResponses(dialogueData.Responses);
        }
        else
        {
            CloseDialogueBox();

        }
    }

    // Displays text
    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, dialogueText);
        
        while (typeWriterEffect.IsRunning)
        {
            yield return null;

            if (inputSystem.OnSelect)
            {
                typeWriterEffect.Stop();
            }
        }
    }

    // leaves the convo
    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueTextArea.SetActive(false);
        dialogueText.text = string.Empty;
    }
}
