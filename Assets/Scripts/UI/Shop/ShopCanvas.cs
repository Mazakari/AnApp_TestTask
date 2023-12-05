using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    public static event Action OnMainMenuButton;

    [Space(10)]
    [Header("Shop Tabs")]
    [SerializeField] private ShopTab _skins;

    private ShopTab[] _shopTabs;

    private ISaveLoadService _saveLoadService;

    private void OnEnable()
    {
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        _shopTabs = GetComponentsInChildren<ShopTab>();

        _skins.TabButton.onClick.AddListener(delegate { SwitchActiveTab(_skins); });
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void OnDisable()
    {
        _skins.TabButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
    }

    private void LoadMainMenu()
    {
        _saveLoadService.SaveProgress();
        OnMainMenuButton?.Invoke();
    }

    private void SwitchActiveTab(ShopTab sender)
    {
        for (int i = 0; i < _shopTabs.Length; i++)
        {
            if (ReferenceEquals(_shopTabs[i], sender))
            {
                if (!_shopTabs[i].IsActive)
                {
                    _shopTabs[i].Show();
                }

                continue;
            }

            _shopTabs[i].Hide();
        }
    }
}
