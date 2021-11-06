using System;
using System.Collections.Generic;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    public List<Item> ItemList =>_itemList;
    
    private List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();
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
}