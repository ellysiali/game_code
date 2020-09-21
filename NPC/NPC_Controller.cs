using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    private DialogueManager dialogueManager;

    [SerializeField] private GameObject DialogueManager;
    [SerializeField] private DialogueList dialogueList;

    public void Start()
    {
        dialogueManager = DialogueManager.GetComponent<DialogueManager>();
        dialogueList.SetActiveIndex(0);
    }

    public void Update()
    {
    }

    public void ActivateDialogue()
    {
        dialogueManager.StartDialogue(dialogueList.GetActiveDialogue());
        if (!dialogueList.CheckEndofList())
        {
            dialogueList.IncrementActiveIndex();
        }
    }

    public bool CheckIfDialogueActive() => dialogueManager.CheckIfDialogueActive();
}
