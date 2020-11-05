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
    }

    public void Update()
    {
    }

    public void ActivateDialogue()
    {
        Debug.Log(dialogueList.activeIndex);
        dialogueManager.StartDialogue(GetActiveDialogue());
        if (!CheckEndofList())
        {
            dialogueList.activeIndex++;
        }
    }
    public void SetActiveIndex(int newIndex)
    {
        dialogueList.activeIndex = newIndex;
    }
    private Dialogue GetActiveDialogue()
    {
        return dialogueList.dialogues[dialogueList.activeIndex];
    }
    private bool CheckEndofList()
    {
        return dialogueList.activeIndex == dialogueList.dialogues.Length - 1;
    }
    public bool CheckIfDialogueActive() => dialogueManager.CheckIfDialogueActive();
}
