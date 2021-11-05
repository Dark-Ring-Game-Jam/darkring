using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private Inventory _inventory;
    private Transform _itemSlotContainer;
    private Transform _itemSlotTemplate;
    private Player _player;

    private void Awake()
    {
        _itemSlotContainer = transform.Find("ItemSlotContainer");
        _itemSlotTemplate = _itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetPlayer(Player player) 
    {
        _player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }
    
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) 
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in _itemSlotContainer) 
        {
            if (child == _itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 75f;
        foreach (Item item in _inventory.ItemList) 
        {
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            /*itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => 
            {
                // Use item
                _inventory.UseItem(item);
            };
            
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => 
            {
                // Drop item
                Item duplicateItem = new Item { Type = item.itemType, amount = 1 };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };*/

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            Image amount = itemSlotRectTransform.Find("Amount").GetComponent<Image>();
            amount.sprite = item.GetAmountSprite();

            x++;
        }
    }
}
