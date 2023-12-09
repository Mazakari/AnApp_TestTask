using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour, ISavedProgress
{
    [SerializeField] private TMP_Text _moneyCounter;
    private int _money;

    private IMetaResourcesService _metaResourcesService;

    private void OnEnable()
    {
        CacheServices();
        SubscribeUICallbacks();

        InitMoneyCounter();
    }

    private void OnDisable() => 
        UnsubscribeUICallbacks();

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
    }

    private void CacheServices() =>
       _metaResourcesService = AllServices.Container.Single<IMetaResourcesService>();

    private void SubscribeUICallbacks() =>
       MetaResourcesService.OnMoneyValueChange += UpdateCounter;
    private void UnsubscribeUICallbacks() =>
       MetaResourcesService.OnMoneyValueChange -= UpdateCounter;
    private void InitMoneyCounter()
    {
        _money = _metaResourcesService.PlayerMoney;
        UpdateCounter(_money);
    }
}
