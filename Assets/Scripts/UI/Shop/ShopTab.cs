using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameMetaData;

public class ShopTab : MonoBehaviour, ISavedProgress
{
    [SerializeField] private ShopItemType _type;
    [SerializeField] private GameObject _body;

    [SerializeField] private GameObject _content;
    public GameObject Content => _content;

    [SerializeField] private Button _tabButton;
    public Button TabButton => _tabButton;

    [SerializeField] private Image _buttonImage;

    [SerializeField] private Color _defaultButtonColor;
    [SerializeField] private Color _disabledButtonColor;

    private IShopService _shopService;
    private ISkinsService _skinService;

    private bool _isActive = false;
    public bool IsActive => _isActive;

    private void OnEnable()
    {
        _shopService = AllServices.Container.Single<IShopService>();
        _skinService = AllServices.Container.Single<ISkinsService>();

        ShopItem.OnShopItemEquipped += SwitchEquippedItemState;
    }

    private void OnDisable() => 
        ShopItem.OnShopItemEquipped -= SwitchEquippedItemState;


    private void SwitchEquippedItemState(ShopItem sender)
    {
        if (sender.Type == ShopItemType.Skin)
        {
            _skinService.SetCurrentSkinPrefab(sender.ItemPrefab);
            UnequipItems(_shopService.SkinItems, sender);
        }
    }

    public void Show()
    {
        ShowItemModelInTab();

        SetButtonColor(_defaultButtonColor);

        _body.SetActive(true);
        _isActive = true;
    }

    public void Hide()
    {
        _shopService.HideAllModelsByType(_type);

        SetButtonColor(_disabledButtonColor);

        _isActive = false;
        _body.SetActive(false);
    }

    private void SetButtonColor(Color newColor) => 
        _buttonImage.color = newColor;

    private void UnequipItems(List<ShopItem> items, ShopItem sender)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (!ReferenceEquals(items[i], sender))
            {
                items[i].Unequip();
                items[i].UpdateState();
            }
        }
    }

    private void ShowItemModelInTab()
    {
        if (_type == ShopItemType.Skin)
        {
            _shopService.ShowEquippedItemModel(_type);
            return;
        }

        _shopService.ShowItemModelByIndex(_type, 0);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        if (gameObject.CompareTag(Constants.SKINS_TAB_TAG))
        {
            // Save current skin
            progress.gameData.currentSkinPrefab = _skinService.CurrentSkinPrefab;

            // Save skin items
            CopyProgress(_shopService.SkinItems, progress.gameData.skins);

            return;
        }
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (gameObject.CompareTag(Constants.SKINS_TAB_TAG))
        {
            if (progress.gameData.skins.Count > 0)
            {
                for (int i = 0; i < progress.gameData.skins.Count; i++)
                {
                    _shopService.SkinItems[i].isLocked = progress.gameData.skins[i].isLocked;
                    _shopService.SkinItems[i].isEquipped = progress.gameData.skins[i].isEquipped;

                    _shopService.SkinItems[i].UpdateState();
                }
            }

            return;
        }
    }

    private void CopyProgress(List<ShopItem> source, List<ShopItemData> target)
    {
        target.Clear();

        for (int i = 0; i < source.Count; i++)
        {
            ShopItemData data = new()
            {
                isLocked = source[i].isLocked,
                isEquipped = source[i].isEquipped
            };

            target.Add(data);
        }
    }
}
