using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Note, // записка
        Candle, // свечка
        InsulatingTape, // изолента
        KeroseneLamp, // керосиновая лампа
        Flashlight, // фонарик
        Key, // ключ
        Batteriy // батарейки
    }

    public ItemType Type;
    public int Amount;
    
    public Sprite GetSprite() 
    {
        switch (Type) 
        {
            case ItemType.Note:        
                return ItemAssets.Instance.NoteSprite;
            case ItemType.Candle: 
                return ItemAssets.Instance.CandleSprite;
            case ItemType.InsulatingTape:   
                return ItemAssets.Instance.InsulatingTapeSprite;
            case ItemType.KeroseneLamp:         
                return ItemAssets.Instance.KeroseneLampSprite;
            case ItemType.Flashlight:       
                return ItemAssets.Instance.FlashlightSprite;
            case ItemType.Key:       
                return ItemAssets.Instance.KeySprite;
            case ItemType.Batteriy:       
                return ItemAssets.Instance.BatterySprite;
            default:
                return ItemAssets.Instance.EmptySprite;
        }
    }

    public Sprite GetAmountSprite()
    {
        if (!IsStackable())
        {
            return ItemAssets.Instance.EmptySprite;
        }
        
        switch (Amount)
        {
            case 1:
                return ItemAssets.Instance.AmountX1;
            case 2:
                return ItemAssets.Instance.AmountX2;
            case 3:
                return ItemAssets.Instance.AmountX3;
            case 4:
                return ItemAssets.Instance.AmountX4;
            case 5:
                return ItemAssets.Instance.AmountX5;
            case 6:
                return ItemAssets.Instance.AmountX6;
            default:
                return ItemAssets.Instance.EmptySprite;
        }
    }

    public bool IsStackable() 
    {
        switch (Type) 
        {
            case ItemType.Note:
            case ItemType.Candle:
            case ItemType.Key:
            case ItemType.Batteriy:
            case ItemType.InsulatingTape:
                return true;
            case ItemType.KeroseneLamp:
            case ItemType.Flashlight:
                return false;
            default:
                return true;
        }
    }
}