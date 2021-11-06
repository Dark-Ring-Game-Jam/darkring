using UnityEngine;

public class ItemAssets : MonoBehaviour 
{
    public static ItemAssets Instance { get; private set; }

    private void Awake() 
    {
        Instance = this;
    }
    
    public Transform ItemWorldPrefab;

    public Sprite NoteSprite;
    public Sprite CandleSprite;
    public Sprite InsulatingTapeSprite;
    public Sprite KeroseneLampSprite;
    public Sprite FlashlightSprite;
    public Sprite KeySprite;
    public Sprite BatterySprite;
    public Sprite EmptySprite;
    
    public Sprite AmountX1;
    public Sprite AmountX2;
    public Sprite AmountX3;
    public Sprite AmountX4;
    public Sprite AmountX5;
    public Sprite AmountX6;
    public Sprite AmountX7;
    public Sprite AmountX8;
    public Sprite AmountX9;
}