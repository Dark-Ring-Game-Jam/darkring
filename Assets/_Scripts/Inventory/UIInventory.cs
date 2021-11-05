using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private Inventory _inventory;

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
    }
}
