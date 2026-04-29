using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    public IReadOnlyList<Item> Items => items;

    public event Action OnInventoryChanged;

    public void AddItem(Item item)
    {
        items.Add(item);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        OnInventoryChanged?.Invoke();
    }
}