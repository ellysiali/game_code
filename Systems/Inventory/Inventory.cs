using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "newInventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> itemList = new List<InventorySlot>();
    public void AddItem(Item item, int amount)
    {
        bool itemFound = false;
        foreach (InventorySlot slot in itemList)
        {
            if (item.name == slot.item.name)
            {
                itemFound = true;
                slot.AddAmount(amount);
            }
        }
        if (!itemFound)
        {
            itemList.Add(new InventorySlot(item, amount));
        }
    }
    public void RemoveItem(Item item, int amount)
    {
        int selectedIndex = 0;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].item.name == item.name)
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

    public int CheckAmount (Item item)
    {
        bool itemFound = false;
        foreach (InventorySlot slot in itemList)
        {
            if (item.name == slot.item.name)
            {
                itemFound = true;
                return slot.amount;
            }
        }
        if (!itemFound)
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
    public InventorySlot (Item item, int amount)
    {
        this.item = item;
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