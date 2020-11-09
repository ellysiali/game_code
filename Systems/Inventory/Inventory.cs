using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Dictionary<string, int> items = new Dictionary<string, int>();
    public void AddItem(string name, int amount)
    {
        if (items.ContainsKey(name))
        {
            items[name] += amount;
        }
        else
        {
            items.Add(name, amount);
        }
    }
    public void RemoveItem(string name, int amount)
    {
        items[name] -= amount;
        if (items[name] <= 0)
        {
            items.Remove(name);
        }
    }
    public int CheckAmount (string name)
    {
        items.TryGetValue(name, out int amount);
        return amount;
    }
}