using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue: ScriptableObject
{
    public DialogueLine[] lines;
    public Question question;
}

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    [TextArea(3, 10)]
    public string dialogue;
    public Sprite portrait;
}