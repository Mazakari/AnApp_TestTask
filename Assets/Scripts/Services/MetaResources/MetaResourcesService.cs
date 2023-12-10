using System;

public class MetaResourcesService : IMetaResourcesService
{
    public static event Action<float> OnMoneyValueChange;

    private readonly ISaveLoadService _saveLoadService;

    public MetaResourcesService(ISaveLoadService saveLoadService) => 
        _saveLoadService = saveLoadService;

    private float _playerMoney;
    public float PlayerMoney
    { 
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
