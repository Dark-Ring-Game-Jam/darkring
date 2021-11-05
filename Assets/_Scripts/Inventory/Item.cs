using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Note, // записка
        Candle, // свечка
        ElectricalTape, // изолента
        KeroseneLamp, // керосиновая лампа
        Flashlight, // фонарик
        Key, // ключ
        Batteries, // батарейки
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
            case ItemType.ElectricalTape:   
                return ItemAssets.Instance.ElectricalTapeSprite;
            case ItemType.KeroseneLamp:         
                return ItemAssets.Instance.KeroseneLampSprite;
            case ItemType.Flashlight:       
                return ItemAssets.Instance.FlashlightSprite;
            case ItemType.Key:       
                return ItemAssets.Instance.KeySprite;
            case ItemType.Batteries:       
                return ItemAssets.Instance.BatteriesSprite;
            default:
                return ItemAssets.Instance.EmptySprite;
        }
    }

    public Sprite GetAmountSprite()
    {
        switch (Amount)
        {
            case 1:
                return ItemAssets.Instance.AmountX1;
                break;
            case 2:
                return ItemAssets.Instance.AmountX2;
                break;
            case 3:
                return ItemAssets.Instance.AmountX3;
                break;
            case 4:
                return ItemAssets.Instance.AmountX4;
                break;
            case 5:
                return ItemAssets.Instance.AmountX5;
                break;
            case 6:
                return ItemAssets.Instance.AmountX6;
                break;
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
            case ItemType.Batteries:
                return true;
            case ItemType.ElectricalTape:
            case ItemType.KeroseneLamp:
            case ItemType.Flashlight:
                return false;
            default:
                return true;
        }
    }
}