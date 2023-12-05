using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour, ISavedProgress
{
    [SerializeField] private TMP_Text _moneyCounter;
    private int _money;

    private IMetaResourcesService _metaResourcesService;

    private void OnEnable()
    {
        _metaResourcesService = AllServices.Container.Single<IMetaResourcesService>();
        
        ShopItem.OnShopItemBuy += UpdateCounter;

        _money = _metaResourcesService.PlayerMoney;
        UpdateCounter(_money);
    }

    private void OnDisable() => 
        ShopItem.OnShopItemBuy -= UpdateCounter;

    public void UpdateCounter(int newMoneyValue) => 
        _moneyCounter.text = newMoneyValue.ToString();

    public void UpdateProgress(PlayerProgress progress)
    {
        _money = _metaResourcesService.PlayerMoney;
        progress.gameData.playerMoney = _money;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        _money = progress.gameData.playerMoney;
        _metaResourcesService.PlayerMoney = _money;

        UpdateCounter(_money);
    }
}
