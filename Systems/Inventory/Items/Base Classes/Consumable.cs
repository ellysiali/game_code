using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newConsumable", menuName = "Item/Consumable")]

public class Consumable : Item
{
    public float healValue = 0;
    public void Awake()
    {
        type = ItemType.Consumable;
    }
}
