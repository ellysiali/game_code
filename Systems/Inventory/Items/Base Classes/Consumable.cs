using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newConsumable", menuName = "Item/Consumable")]

public class Consumable : Item
{
    public float health = 0f;
    public float magic = 0f;
    public float attack = 0f;
    public float defense = 0f;
    public float healingOverTime = 0f;
    public float magicOverTime = 0f;
    public float buffDuration = 60f;

    public void Awake()
    {
        type = ItemType.Consumable;
    }
}
