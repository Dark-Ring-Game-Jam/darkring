using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> _itemList;
    
    public Inventory()
    {
        _itemList = new List<Item>();
        
        // TODO - для теста (убрать)
        AddItem(new Item { Type = Item.ItemType.Key, Amount = 1 });
        
        Debug.Log("Inventory Items Count: " + _itemList.Count);
    }
    
    public void AddItem(Item item)
    {
        _itemList.Add(item);
        Debug.Log("Inventory Items Count (added): " + _itemList.Count);
    }
}