using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
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

    public bool ContainItemType(Item.ItemType itemType)
    {
        return _itemList.Any(item => item.Type == itemType);
    }

    public int ItemCount(Item.ItemType itemType)
    {
        return TryGetItem(itemType, out var item) ? item.Amount : 0;

    }

    public bool TryGetItem(Item.ItemType itemType, out Item item)
    {
        if (ContainItemType(itemType))
        {
            foreach (var existItem in _itemList.Where(existItem => existItem.Type == itemType))
            {
                item = existItem;

                return true;
            }
        }

        item = default;

        return false;
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