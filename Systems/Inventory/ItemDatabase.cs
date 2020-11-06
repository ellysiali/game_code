using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items = new List<Item>();
}
