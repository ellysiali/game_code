using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
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
        transform.Find("Select").gameObject.SetActive(true);
    }
    public void Deselect()
    {
        isSelected = false;
        transform.Find("Select").gameObject.SetActive(false);
    }
    public void Remove()
    {
        Destroy(gameObject);
    }
}
