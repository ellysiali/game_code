using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Dialogue/Data/Option Data")]

public class OptionData : ScriptableObject
{
    [TextArea(1, 3)]
    public string text;
    public DialogueList newDialogueList = null;
}
