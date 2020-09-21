using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Consumable, Equipment, Default
}

public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    [TextArea(3, 15)]
    public string description;
    public float price;
}
