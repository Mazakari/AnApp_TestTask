using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSkinData", menuName = "StaticData/Shop/Skin")]
public class ShopItemSkinData : ScriptableObject
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
