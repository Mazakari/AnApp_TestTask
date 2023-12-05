using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSkinDataSO", menuName = "Shop/ShopItemData/Skins", order = 2)]
public class ShopItemSkinDataSO : ScriptableObject
{
    [SerializeField] private ShopItemType _type;
    public ShopItemType Type => _type;


    [SerializeField] private Sprite _shopSprite;
    public Sprite Sprite => _shopSprite;


    [SerializeField] private GameObject _skinPrefab;
    public GameObject SkinPrefab => _skinPrefab;


    [SerializeField] private int _unlockCost;
    public int UnlockCost => _unlockCost;


    [SerializeField] private bool _isLocked = true;
    public bool IsLocked { get => _isLocked; set => _isLocked = value; }
}
