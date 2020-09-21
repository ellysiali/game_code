using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEquipment", menuName = "Item/Equipment")]

public class Equipment : Item
{
    public float attackBonus = 0;
    public float defenseBonus = 0;

    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
