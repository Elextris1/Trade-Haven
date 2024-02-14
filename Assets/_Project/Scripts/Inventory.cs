using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> itemsInInventory = new List<Item>();
    [SerializeField] private int maxSpace;
    public bool HasSpace => itemsInInventory.Count < maxSpace;

    public event Action OnItemAdded;
    public event Action OnItemRemoved;

    [Serializable]
    public class Item
    {
        public SO_ItemInfo info;
        public int amountStored;
    }

    public List<Item> GetItems()
    {
        return new List<Item>(itemsInInventory);
    }

    private void AddItem(SO_ItemInfo addedItem, int addedAmount)
    {
        if (addedItem.isStackable)
        {
            foreach (var item in itemsInInventory)
            {
                if (item.info == addedItem)
                {
                    item.amountStored += addedAmount;
                    OnItemAdded?.Invoke();
                    return;
                }
            }
        }

        var newitem = new Item();
        newitem.info = addedItem;
        newitem.amountStored = addedAmount;
        itemsInInventory.Add(newitem);
        OnItemAdded?.Invoke();
    }

    private void RemoveItem(Item itemToRemove, int amountToRemove)
    {
        itemToRemove.amountStored -= amountToRemove;
        if (itemToRemove.amountStored < 1)
        {
            itemsInInventory.Remove(itemToRemove);
        }
        OnItemRemoved?.Invoke();
    }

    public bool TryAddItem(SO_ItemInfo newItem, int amount)
    {
        if (newItem.isStackable)
        {
            foreach (var itemInInventory in itemsInInventory)
            {
                if (newItem == itemInInventory.info)
                {
                    AddItem(newItem, amount);
                    return true;
                }
            }
        }
        if (HasSpace)
        {
            AddItem(newItem, amount);
            return true;
        }
        return false;
    }

    public bool RequestItem(SO_ItemInfo requestedItem, int requestedAmount)
    {
        foreach (var item in itemsInInventory)
        {
            if (item.info == requestedItem)
            {
                if (item.amountStored >= requestedAmount)
                {
                    RemoveItem(item, requestedAmount);
                    return true;
                }
            }
        }
        return false;
    }
}

