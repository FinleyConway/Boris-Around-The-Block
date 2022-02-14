using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Awake()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    // dynamic choice shower which shows all the possible choices that the player can make
    public void ShowResponses(DialogueResponse[] responses)
    {
        float responseBoxHeight = 0;

        for (int i = 0; i < responses.Length; i++)
        {
            DialogueResponse dialogueResponse = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = dialogueResponse.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickResponse(dialogueResponse, responseIndex));
            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(tempResponseButtons[0]);
    }

    // alt way of not doing OnClick() in inspector but in code and when selecting a choice moves on to the next dialogue option
    private void OnPickResponse(DialogueResponse dialogueResponse, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        responseEvents = null;

        if (dialogueResponse.DialogueData)
        { 
            dialogueUI.ShowDialogue(dialogueResponse.DialogueData);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }
}
