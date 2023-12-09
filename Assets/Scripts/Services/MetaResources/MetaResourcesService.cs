using System;

public class MetaResourcesService : IMetaResourcesService
{
    public static event Action<int> OnMoneyValueChange;

    private readonly ISaveLoadService _saveLoadService;

    public MetaResourcesService(ISaveLoadService saveLoadService) => 
        _saveLoadService = saveLoadService;

    private int _playerMoney;
    public int PlayerMoney{ 
        get => _playerMoney;

        set
        {
            _playerMoney = value;
            SendMoneyValueChangeCallback();
            SaveProgress();
        }
    }

    private void SendMoneyValueChangeCallback() => 
        OnMoneyValueChange?.Invoke(_playerMoney);

    private void SaveProgress() =>
       _saveLoadService.SaveProgress();
}
