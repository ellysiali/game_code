using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private int activeIndex;

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
        GameStatus.GetInstance().NPCIndexes.TryGetValue(gameObject.name + "_index", out activeIndex);
        if (activeIndex == 0)
        {
            activeIndex = PlayerPrefs.GetInt(gameObject.name + "_index", 0);
        }

        dialogueManager.StartDialogue(GetActiveDialogue());
        if (!CheckEndofList())
        {
            activeIndex++;
            SetActiveIndex(activeIndex);
        }
    }
    public void SetActiveIndex(int newIndex)
    {
        try
        {
            GameStatus.GetInstance().NPCIndexes.Add(gameObject.name + "_index", newIndex);
        }
        catch
        {
            GameStatus.GetInstance().NPCIndexes[gameObject.name + "_index"] = newIndex;
        }
    }
    private Dialogue GetActiveDialogue()
    {
        return dialogueList.dialogues[activeIndex];
    }
    private bool CheckEndofList()
    {
        return activeIndex == dialogueList.dialogues.Length - 1;
    }
    public bool CheckIfDialogueActive() => dialogueManager.CheckIfDialogueActive();
}
