using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemStaticData", menuName = "StaticData/Shop/ItemData")]
public class ShopItemStaticData : ScriptableObject
{
    public ShopItemType Type;

    [Space(10)]
    [Header("IAP Purchase")]
    public bool iAPItem = false;

    [Space(10)]
    [Header("Unlock Requirements")]
    public bool needToUnlock = false;
    public int unlocksOnLevel = 0;

    [Space(10)]
    [Header("Info Block")]
    public Sprite itemSprite;
    [Space(5)]
    public Sprite valueImage;
    public int valueAmount;

    [Space(10)]
    [Header("Price Block")]
    public string itemName;

    [Space(5)]
    public Sprite costCurrencyImage;
    public float costAmount;
}
