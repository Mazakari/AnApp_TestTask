using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public bool IsEquipped { get; set; } = false;
    public bool NeedToUnlock { get; set; } = false;

    public ShopItemType Type { get; private set; }

    [Space(10)]
    [Header("Unlock Requirements")]
    private int _unlocksOnLevel;

    [Header("Info Block")]
    [SerializeField] private Image _itemInfoImage;
    [Space(5)]
    [SerializeField] private Image _itemInfoValueImage;
    [SerializeField] private TMP_Text _itemInfoValue_Text;

    [Space(10)]
    [Header("Price Block")]
    [SerializeField] private TMP_Text _itemName_Text;
    [Space(5)]
    [SerializeField] private Button _unlockButton;

    [Space(5)]
    [Header("Cost")]
    [SerializeField] private GameObject _costParent;
    [Space(5)]
    [SerializeField] private Image _costCurrencyImage;
    [SerializeField] private TMP_Text _costCurrencyText;
    private float _unlockCost;
    private bool _iAPItem = false;
    private int _iAPValue = 0;

    [Space(5)]
    [Header("Equipped")]
    [SerializeField] private GameObject _equippedParent;

    [SerializeField] private ShopItemSounds _itemSounds; 

    private IMetaResourcesService _metaResourcesService;
    private ILevelService _levelService;

    private void OnEnable() => 
        CacheServices();

    public void InitItemWithStaticData(ShopItemStaticData data)
    {
        try
        {
            Type = data.Type;

            _iAPItem = data.iAPItem;
            _iAPValue = data.valueAmount;

            _itemInfoImage.sprite = data.itemSprite;

            _itemInfoValueImage.sprite = data.valueImage;
            _itemInfoValue_Text.text = $"x{data.valueAmount}";

            if (_itemInfoValueImage.sprite == null)
            {
                _itemInfoValueImage.enabled = false;
            }
            if (data.valueAmount == 0)
            {
                _itemInfoValue_Text.enabled = false;
            }


            _itemName_Text.text = data.itemName;

            _costCurrencyImage.sprite = data.costCurrencyImage;
            if (_costCurrencyImage.sprite == null && _iAPItem)
            {
                _costCurrencyImage.enabled = false;
            }

            _unlockCost = data.costAmount;
            _costCurrencyText.text = $"{_unlockCost}";

            NeedToUnlock = data.needToUnlock;
            _unlocksOnLevel = data.unlocksOnLevel;
            IsEquipped = false;

            SwitchToBought(false);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    public void BuyItem()
    {
        try
        {
            if (NeedToUnlock)
            {
                if (CurrentLevelLowerThenRequired())
                {
                    Debug.Log($"Item is locked. Need to complete level {_unlocksOnLevel}. Currently completed level is {_levelService.CurrentLevelBuildIndex}");
                    return;
                }
            }

            if (_iAPItem)
            {
                Debug.Log("IAP Purchase button");
                _metaResourcesService.PlayerMoney += _iAPValue;
                SwitchToBought(true);
                DisableBuyButton();
                return;
            }

            float money = _metaResourcesService.PlayerMoney;

            if (money < _unlockCost)
            {
                Debug.Log("Not enough money");
                return;
            }

            BuyAndMarkAsOwned();
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    private void BuyAndMarkAsOwned()
    {
        DeductCostFromPlayerMoney();
        SwitchToBought(true);
        DisableBuyButton();
    }
   
    private void DeductCostFromPlayerMoney() => 
        _metaResourcesService.PlayerMoney -= _unlockCost;
    private void SwitchToBought(bool bought)
    {
        _costParent.SetActive(!bought);
        _equippedParent.SetActive(bought);
    }
    private void DisableBuyButton() =>
       _unlockButton.interactable = false;

    private void CacheServices()
    {
        _metaResourcesService = AllServices.Container.Single<IMetaResourcesService>();
        _levelService = AllServices.Container.Single<ILevelService>();
    }

    private bool CurrentLevelLowerThenRequired() =>
       _levelService.CurrentLevelBuildIndex < _unlocksOnLevel - 1;
}
