using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StoreData", menuName = "Dialogue/StoreData")]
public class StoreData : ScriptableObject
{
    public ItemDatabase inventory;
    public Dialogue welcomeDialogue;
    public Dialogue purchaseDialogue;
    public Dialogue noMoneyDialogue;
}
