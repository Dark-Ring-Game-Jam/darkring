using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour 
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) 
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorldPrefab, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item) 
    {
        Vector3 randomDir = GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 8f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 40f, ForceMode2D.Impulse);
        return itemWorld;
    }
    
    private static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
    }


    private Item _item;
    private SpriteRenderer _spriteRenderer;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item) 
    {
        this._item = item;
        _spriteRenderer.sprite = item.GetSprite();
        if (item.Amount > 1) 
        {
            // TODO - заменить на Image
            // _textMeshPro.SetText(item.Amount.ToString());
        } 
        else 
        {
            // TODO - заменить на Image
            // _textMeshPro.SetText("");
        }
    }

    public Item GetItem() 
    {
        return _item;
    }

    public void DestroySelf() 
    {
        Destroy(gameObject);
    }
}