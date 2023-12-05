using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ShopItemType Type { get; private set; }

    [SerializeField] private Image _itemImage;

    private GameObject _itemPrefab;
    public GameObject ItemPrefab => _itemPrefab;

    public GameObject ItemModel { get; set; }

    private int _unlockCost;
    [SerializeField] private TMP_Text _unlockCost_Text;
    [SerializeField] private TMP_Text _equiped_Text;
    
    [SerializeField] private Button _unlockButton;
    [SerializeField] private Button _equipButton;

    [SerializeField] private ShopItemSounds _itemSounds; 

    private ISkinsService _skinsService;
    private IShopService _shopService;
    private IMetaResourcesService _metaResourcesService;

    [HideInInspector] public bool isEquipped = false;
    [HideInInspector] public bool isLocked = false;

    public static event Action<ShopItem> OnShopItemEquipped;
    public static event Action<int> OnShopItemBuy;

    private void OnEnable()
    {
        _skinsService = AllServices.Container.Single<ISkinsService>();
        _shopService = AllServices.Container.Single<IShopService>();
        _metaResourcesService = AllServices.Container.Single<IMetaResourcesService>();

        _unlockButton.onClick.AddListener(Unlock);
        _equipButton.onClick.AddListener(Equip);
    }

    private void OnDisable()
    {
        _unlockButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
    }

    public void InitSkinItem(ShopItemSkinDataSO itemDataSO)
    {
        try
        {
            Type = itemDataSO.Type;
            _itemImage.sprite = itemDataSO.Sprite;
            _itemPrefab = itemDataSO.SkinPrefab;
            _unlockCost = itemDataSO.UnlockCost;
            _unlockCost_Text.text = _unlockCost.ToString();
            isLocked = itemDataSO.IsLocked;

            UpdateState();
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
   
    public void Unequip()
    {
        if (!isLocked && isEquipped)
        {
            // Unequip item
            SwitchEquiped(false);
        }
    }

    public void UpdateState()
    {
        _unlockButton.gameObject.SetActive(true);
        _equiped_Text.gameObject.SetActive(false);

        if (!isLocked)
        {
            _unlockButton.gameObject.SetActive(false);

            if (isEquipped)
            {
                SetCurrentSkin();
                OnShopItemEquipped?.Invoke(this);

                _equiped_Text.gameObject.SetActive(true);
            }

            return;
        }
    }

    private void Unlock()
    {
        if (isLocked)
        {
            // if player enough money
            
            int money = _metaResourcesService.PlayerMoney;
            if(money >= _unlockCost)
            {
                // Deduct player money
                money -= _unlockCost;
                _metaResourcesService.PlayerMoney = money;

                // Send callback for PlayerMoney to updte money counter
                OnShopItemBuy?.Invoke(money);

                // Unlock item
                isLocked = false;
                _unlockButton.gameObject.SetActive(false);

                // Play unlock sound
                _itemSounds.PlayUnlockSound();

                return;
            }
        }

        Debug.Log("Not enough money");
    }

    private void Equip()
    {
        if (!isLocked)
        {
            if (Type == ShopItemType.Skin)
            {
                if (!isEquipped)
                {
                    // Set item as currently equipped
                    SetCurrentSkin();

                    _itemSounds.PlayEquipSound();

                    // Send event OnItemEquip? To Unequip currently equipped item
                    OnShopItemEquipped?.Invoke(this);

                    SwitchEquiped(true);
                    UpdateState();
                }
            }
        }
    }

    private void SetCurrentSkin() => 
        _skinsService.SetCurrentSkinPrefab(_itemPrefab);

    private void SwitchEquiped(bool isEquiped) =>
        isEquipped = isEquiped;
}
