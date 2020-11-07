using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    // TODO: potentially change base/derived class from list to dict
    public List<InventorySlot> itemList;
    public Inventory()
    {
        itemList = new List<InventorySlot>();
    }
    public void AddItem(int itemID, int amount)
    {
        bool itemFound = false;
        foreach (InventorySlot slot in itemList)
        {
            if (itemID == slot.itemID)
            {
                itemFound = true;
                slot.AddAmount(amount);
            }
        }
        if (!itemFound)
        {
            itemList.Add(new InventorySlot(itemID, amount));
        }
    }
    public void RemoveItem(int itemID, int amount)
    {
        int selectedIndex = 0;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemID == itemID)
            {
                itemList[i].RemoveAmount(amount);
                selectedIndex = i;
            }
        }
        if (itemList[selectedIndex].amount == 0)
        {
            itemList.Remove(itemList[selectedIndex]);
        }
    }
    public int CheckAmount (int itemID)
    {
        foreach (InventorySlot slot in itemList)
        {
            if (itemID == slot.itemID)
            {
                return slot.amount;
            }
        }
        return 0;
    }
}

[System.Serializable]
public class InventorySlot
{
    public int itemID;
    public int amount;
    public InventorySlot (int itemID, int amount)
    {
        this.itemID = itemID;
        this.amount = amount;
    }

    public void AddAmount (int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        amount -= value;
        if (amount < 0)
        {
            Debug.LogError("Error: removing more items than in inventory");
        }
    }
}