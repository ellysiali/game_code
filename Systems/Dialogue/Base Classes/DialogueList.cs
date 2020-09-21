using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue List", menuName = "Dialogue/Dialogue List")]

public class DialogueList : ScriptableObject
{
    [SerializeField] public Dialogue[] dialogues;
    public int activeIndex = 0;

    public Dialogue GetActiveDialogue()
    {
        return dialogues[activeIndex];
    }

    public void IncrementActiveIndex()
    {
        activeIndex++;
    }

    public void SetActiveIndex(int newIndex)
    {
        activeIndex = newIndex;
    }

    public bool CheckEndofList()
    {
        return activeIndex == dialogues.Length - 1;
    }
}
