using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogueData;

    public void UpdateDialogueData(DialogueData dialogueData)
    {
        this.dialogueData = dialogueData;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            if (player.Interactable is DialogueTrigger dialogueTrigger && dialogueTrigger == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerController player)
    {
        foreach (DialogueResponseEvent responseEvent in GetComponents<DialogueResponseEvent>())
        {
            if (responseEvent.DialogueData == dialogueData)
            {
                player.DialogueUI.AddResponseEvents(responseEvent.Events);
                break;
            }
        }

        player.DialogueUI.ShowDialogue(dialogueData);
    }
}
