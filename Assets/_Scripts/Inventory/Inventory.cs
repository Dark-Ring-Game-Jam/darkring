using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    public List<Item> ItemList =>_itemList;
    
    private List<Item> _itemList;
    private Action<Item> _useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        _useItemAction = useItemAction;
        _itemList = new List<Item>();
        
        // TODO - для теста (убрать)
        AddItem(new Item { Type = Item.ItemType.Key, Amount = 1 });
        AddItem(new Item { Type = Item.ItemType.Candle, Amount = 2 });
        AddItem(new Item { Type = Item.ItemType.Note, Amount = 1 });
        
        Debug.Log("Inventory Items Count: " + _itemList.Count);
    }
    
    public void AddItem(Item item)
    {
        if (item.IsStackable()) 
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in _itemList) 
            {
                if (inventoryItem.Type == item.Type) 
                {
                    inventoryItem.Amount += item.Amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory) 
            {
                _itemList.Add(item);
            }
        } 
        else 
        {
            _itemList.Add(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void RemoveItem(Item item) 
    {
        if (item.IsStackable()) 
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in _itemList) 
            {
                if (inventoryItem.Type == item.Type) 
                {
                    inventoryItem.Amount -= item.Amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.Amount <= 0) 
            {
                _itemList.Remove(itemInInventory);
            }
        } 
        else 
        {
            _itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item) {
        _useItemAction(item);
    }
}