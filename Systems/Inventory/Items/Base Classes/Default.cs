using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDefault", menuName = "Item/Default")]
public class Default : Item
{
    public void Awake()
    {
        type = ItemType.Default;
    }
}
