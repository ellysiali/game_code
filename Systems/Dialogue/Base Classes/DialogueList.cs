using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue List", menuName = "Dialogue/Dialogue List")]

public class DialogueList : ScriptableObject
{
    public Dialogue[] dialogues;
    public int activeIndex = 0;
}
