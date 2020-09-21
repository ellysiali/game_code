using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    public bool isSelected;
    public bool isActive;
    public Item item;

    public void Awake()
    {
        isSelected = isActive = false;
    }
    public void Select()
    {
        isSelected = true;
        transform.Find("Frame").GetComponent<Image>().color = Color.yellow;
    }
    public void Deselect()
    {
        isSelected = false;
        transform.Find("Frame").GetComponent<Image>().color = Color.white;
    }
    public void Remove()
    {
        Destroy(gameObject);
    }
}
