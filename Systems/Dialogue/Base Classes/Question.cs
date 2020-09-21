using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Dialogue/Question")]

public class Question : ScriptableObject
{
    public DialogueLine dialogue;
    public OptionObject[] options;
}

[System.Serializable]
public class OptionObject
{
    public string option;
    public ScriptableObject action = null;
}