using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool isPlayerInRange;

    private void Awake()
    {
        isPlayerInRange = false;
        visualCue.SetActive(true);
    }

    private void Update()
    {
        if (isPlayerInRange && !DialogueManager.instance.isDialoguePlaying)
        {
            visualCue.SetActive(false);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.instance.EnterDialogueMode(inkJSON);
            }
        }
        else if (!isPlayerInRange)
        {
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
        Vector3 lookPos = other.transform.position - npc.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, rotation, Time.deltaTime * 5);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
