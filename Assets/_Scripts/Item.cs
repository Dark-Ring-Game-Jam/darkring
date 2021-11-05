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
}